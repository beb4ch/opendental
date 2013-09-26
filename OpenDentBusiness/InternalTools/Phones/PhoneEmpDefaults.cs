using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Not a true Cache pattern.  It only loads the cache once on startup and then never again.  No entry in the Cache file.  No InvalidType for PhoneEmpDefault.  Data is almost always pulled from db in realtime, and this cache is only used for default ringgroups.</summary>
	public class PhoneEmpDefaults{
		#region CachePattern

		///<summary>A list of all PhoneEmpDefaults.</summary>
		private static List<PhoneEmpDefault> listt;

		///<summary>A list of all PhoneEmpDefaults.</summary>
		public static List<PhoneEmpDefault> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary>Not part of the true Cache pattern.  See notes above.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM phoneempdefault";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PhoneEmpDefault";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.PhoneEmpDefaultCrud.TableToList(table);
		}
		#endregion
		
		///<summary></summary>
		public static List<PhoneEmpDefault> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneEmpDefault>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phoneempdefault ORDER BY PhoneExt";//because that's the order we are used to in the phone panel.
			return Crud.PhoneEmpDefaultCrud.SelectMany(command);
		}
		
		public static bool IsGraphed(long employeeNum,List<PhoneEmpDefault> listPED) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listPED.Count;i++) {
				if(listPED[i].EmployeeNum==employeeNum) {
					return listPED[i].IsGraphed;
				}
			}
			return false;
		}

		public static PhoneEmpDefault GetEmpDefault(long employeeNum,List<PhoneEmpDefault> listPED) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listPED.Count;i++) {
				if(listPED[i].EmployeeNum==employeeNum) {
					return listPED[i];
				}
			}
			return null;
		}

		///<summary>Can return null.</summary>
		public static PhoneEmpDefault GetByExtAndEmp(int extension,long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PhoneEmpDefault>(MethodBase.GetCurrentMethod(),extension,employeeNum);
			}
			string command="SELECT * FROM phoneempdefault WHERE PhoneExt="+POut.Int(extension)+" "
				+"AND EmployeeNum="+POut.Long(employeeNum);
			return Crud.PhoneEmpDefaultCrud.SelectOne(command);
		}
		
		public static AsteriskRingGroups GetRingGroup(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].RingGroups;
				}
			}
			return AsteriskRingGroups.All;
		}

		///<summary>Was in phoneoverrides.  Sets the user's ext, name and status override.</summary>
		public static void SetAvailable(int extension,long empNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),extension,empNum);
				return;
			}
			Employee emp=Employees.GetEmp(empNum);
			if(emp==null) {//Should never happen. This means the employee that's changing their status doesn't exist in the employee table.
				return;
			}
			string command="UPDATE phoneempdefault "
				+"SET StatusOverride="+POut.Int((int)PhoneEmpStatusOverride.None)
				+",PhoneExt="+POut.Int(extension)
				+",EmpName='"+POut.String(emp.FName)+"' "
				//No longer require users to manually type in extensions.  This could be the first time a user is going to use this extension so we cannot filter by it.  
				//+"WHERE PhoneExt="+POut.Int(extension)+" "
				+"WHERE EmployeeNum="+POut.Long(empNum);
			Db.NonQ(command);
			//Set the extension to 0 for any other employee that is using this extension to prevent duplicate rows using the same extentions. This would cause confusion for the ring groups.  This is possible if a user logged off and another employee logs into their computer.
			command="UPDATE phoneempdefault SET PhoneExt=0"
				+" WHERE PhoneExt="+POut.Int(extension)
				+" AND EmployeeNum!="+POut.Long(empNum);
			Db.NonQ(command);
		}
	
		///<summary></summary>
		public static long Insert(PhoneEmpDefault phoneEmpDefault){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				phoneEmpDefault.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),phoneEmpDefault);
				return phoneEmpDefault.EmployeeNum;
			}
			return Crud.PhoneEmpDefaultCrud.Insert(phoneEmpDefault,true);//user specifies the PK
		}

		///<summary></summary>
		public static void Update(PhoneEmpDefault phoneEmpDefault){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneEmpDefault);
				return;
			}
			Crud.PhoneEmpDefaultCrud.Update(phoneEmpDefault);
		}

		///<summary></summary>
		public static void Delete(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum);
				return;
			}
			string command= "DELETE FROM phoneempdefault WHERE EmployeeNum = "+POut.Long(employeeNum);
			Db.NonQ(command);
		}

		/// <summary>sorting class used to sort PhoneEmpDefault in various ways</summary>
		public class SortEmpDefaults : IComparer<PhoneEmpDefault> {
			public enum SortyBy { empName, ext, empNum };
			private SortyBy _sortBy = SortyBy.empName;
			public SortEmpDefaults(SortyBy sortBy) {
				_sortBy=sortBy;
			}
			public int Compare(PhoneEmpDefault x,PhoneEmpDefault y) {
				switch(_sortBy) {
					case SortyBy.empNum:
						return x.EmployeeNum.CompareTo(y.EmployeeNum);
					case SortyBy.ext:
						return x.PhoneExt.CompareTo(y.PhoneExt);
					case SortyBy.empName:
					default:
						return x.EmpName.CompareTo(y.EmpName);
				}
			}
		}
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		

		///<summary>Gets one PhoneEmpDefault from the db.</summary>
		public static PhoneEmpDefault GetOne(long employeeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PhoneEmpDefault>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			return Crud.PhoneEmpDefaultCrud.SelectOne(employeeNum);
		}

		
		*/



	}
}