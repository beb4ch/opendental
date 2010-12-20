//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class LanguageForeignCrud {
		///<summary>Gets one LanguageForeign object from the database using the primary key.  Returns null if not found.</summary>
		internal static LanguageForeign SelectOne(long languageForeignNum){
			string command="SELECT * FROM languageforeign "
				+"WHERE LanguageForeignNum = "+POut.Long(languageForeignNum)+" LIMIT 1";
			List<LanguageForeign> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one LanguageForeign object from the database using a query.</summary>
		internal static LanguageForeign SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<LanguageForeign> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of LanguageForeign objects from the database using a query.</summary>
		internal static List<LanguageForeign> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<LanguageForeign> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<LanguageForeign> TableToList(DataTable table){
			List<LanguageForeign> retVal=new List<LanguageForeign>();
			LanguageForeign languageForeign;
			for(int i=0;i<table.Rows.Count;i++) {
				languageForeign=new LanguageForeign();
				languageForeign.LanguageForeignNum= PIn.Long  (table.Rows[i]["LanguageForeignNum"].ToString());
				languageForeign.ClassType         = PIn.String(table.Rows[i]["ClassType"].ToString());
				languageForeign.English           = PIn.String(table.Rows[i]["English"].ToString());
				languageForeign.Culture           = PIn.String(table.Rows[i]["Culture"].ToString());
				languageForeign.Translation       = PIn.String(table.Rows[i]["Translation"].ToString());
				languageForeign.Comments          = PIn.String(table.Rows[i]["Comments"].ToString());
				retVal.Add(languageForeign);
			}
			return retVal;
		}

		///<summary>Inserts one LanguageForeign into the database.  Returns the new priKey.</summary>
		internal static long Insert(LanguageForeign languageForeign){
			return Insert(languageForeign,false);
		}

		///<summary>Inserts one LanguageForeign into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(LanguageForeign languageForeign,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				languageForeign.LanguageForeignNum=ReplicationServers.GetKey("languageforeign","LanguageForeignNum");
			}
			string command="INSERT INTO languageforeign (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="LanguageForeignNum,";
			}
			command+="ClassType,English,Culture,Translation,Comments) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(languageForeign.LanguageForeignNum)+",";
			}
			command+=
				 "'"+POut.String(languageForeign.ClassType)+"',"
				+"'"+POut.String(languageForeign.English)+"',"
				+"'"+POut.String(languageForeign.Culture)+"',"
				+"'"+POut.String(languageForeign.Translation)+"',"
				+"'"+POut.String(languageForeign.Comments)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				languageForeign.LanguageForeignNum=Db.NonQ(command,true);
			}
			return languageForeign.LanguageForeignNum;
		}

		///<summary>Updates one LanguageForeign in the database.</summary>
		internal static void Update(LanguageForeign languageForeign){
			string command="UPDATE languageforeign SET "
				+"ClassType         = '"+POut.String(languageForeign.ClassType)+"', "
				+"English           = '"+POut.String(languageForeign.English)+"', "
				+"Culture           = '"+POut.String(languageForeign.Culture)+"', "
				+"Translation       = '"+POut.String(languageForeign.Translation)+"', "
				+"Comments          = '"+POut.String(languageForeign.Comments)+"' "
				+"WHERE LanguageForeignNum = "+POut.Long(languageForeign.LanguageForeignNum)+" LIMIT 1";
			Db.NonQ(command);
		}

		///<summary>Updates one LanguageForeign in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(LanguageForeign languageForeign,LanguageForeign oldLanguageForeign){
			string command="";
			if(languageForeign.ClassType != oldLanguageForeign.ClassType) {
				if(command!=""){ command+=",";}
				command+="ClassType = '"+POut.String(languageForeign.ClassType)+"'";
			}
			if(languageForeign.English != oldLanguageForeign.English) {
				if(command!=""){ command+=",";}
				command+="English = '"+POut.String(languageForeign.English)+"'";
			}
			if(languageForeign.Culture != oldLanguageForeign.Culture) {
				if(command!=""){ command+=",";}
				command+="Culture = '"+POut.String(languageForeign.Culture)+"'";
			}
			if(languageForeign.Translation != oldLanguageForeign.Translation) {
				if(command!=""){ command+=",";}
				command+="Translation = '"+POut.String(languageForeign.Translation)+"'";
			}
			if(languageForeign.Comments != oldLanguageForeign.Comments) {
				if(command!=""){ command+=",";}
				command+="Comments = '"+POut.String(languageForeign.Comments)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE languageforeign SET "+command
				+" WHERE LanguageForeignNum = "+POut.Long(languageForeign.LanguageForeignNum)+" LIMIT 1";
			Db.NonQ(command);
		}

		///<summary>Deletes one LanguageForeign from the database.</summary>
		internal static void Delete(long languageForeignNum){
			string command="DELETE FROM languageforeign "
				+"WHERE LanguageForeignNum = "+POut.Long(languageForeignNum)+" LIMIT 1";
			Db.NonQ(command);
		}

	}
}