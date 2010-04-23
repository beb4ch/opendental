//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class TaskAncestorCrud {
		///<summary>Gets one TaskAncestor object from the database using the primary key.  Returns null if not found.</summary>
		internal static TaskAncestor SelectOne(long taskAncestorNum){
			string command="SELECT * FROM taskancestor "
				+"WHERE TaskAncestorNum = "+POut.Long(taskAncestorNum);
			List<TaskAncestor> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one TaskAncestor object from the database using a query.</summary>
		internal static TaskAncestor SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TaskAncestor> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one TaskAncestor object from the database using a query.</summary>
		internal static List<TaskAncestor> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TaskAncestor> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<TaskAncestor> TableToList(DataTable table){
			List<TaskAncestor> retVal=new List<TaskAncestor>();
			TaskAncestor taskAncestor;
			for(int i=0;i<table.Rows.Count;i++) {
				taskAncestor=new TaskAncestor();
				taskAncestor.TaskAncestorNum= PIn.Long  (table.Rows[i]["TaskAncestorNum"].ToString());
				taskAncestor.TaskNum        = PIn.Long  (table.Rows[i]["TaskNum"].ToString());
				taskAncestor.TaskListNum    = PIn.Long  (table.Rows[i]["TaskListNum"].ToString());
				retVal.Add(taskAncestor);
			}
			return retVal;
		}

		///<summary>Inserts one TaskAncestor into the database.  Returns the new priKey.</summary>
		internal static long Insert(TaskAncestor taskAncestor){
			return Insert(taskAncestor,false);
		}

		///<summary>Inserts one TaskAncestor into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(TaskAncestor taskAncestor,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				taskAncestor.TaskAncestorNum=ReplicationServers.GetKey("taskancestor","TaskAncestorNum");
			}
			string command="INSERT INTO taskancestor (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="TaskAncestorNum,";
			}
			command+="TaskNum,TaskListNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(taskAncestor.TaskAncestorNum)+",";
			}
			command+=
				     POut.Long  (taskAncestor.TaskNum)+","
				+    POut.Long  (taskAncestor.TaskListNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				taskAncestor.TaskAncestorNum=Db.NonQ(command,true);
			}
			return taskAncestor.TaskAncestorNum;
		}

		///<summary>Updates one TaskAncestor in the database.</summary>
		internal static void Update(TaskAncestor taskAncestor){
			string command="UPDATE taskancestor SET "
				+"TaskNum        =  "+POut.Long  (taskAncestor.TaskNum)+", "
				+"TaskListNum    =  "+POut.Long  (taskAncestor.TaskListNum)+" "
				+"WHERE TaskAncestorNum = "+POut.Long(taskAncestor.TaskAncestorNum);
			Db.NonQ(command);
		}

		///<summary>Updates one TaskAncestor in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(TaskAncestor taskAncestor,TaskAncestor oldTaskAncestor){
			string command="";
			if(taskAncestor.TaskNum != oldTaskAncestor.TaskNum) {
				if(command!=""){ command+=",";}
				command+="TaskNum = "+POut.Long(taskAncestor.TaskNum)+"";
			}
			if(taskAncestor.TaskListNum != oldTaskAncestor.TaskListNum) {
				if(command!=""){ command+=",";}
				command+="TaskListNum = "+POut.Long(taskAncestor.TaskListNum)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE taskancestor SET "+command
				+" WHERE TaskAncestorNum = "+POut.Long(taskAncestor.TaskAncestorNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one TaskAncestor from the database.</summary>
		internal static void Delete(long taskAncestorNum){
			string command="DELETE FROM taskancestor "
				+"WHERE TaskAncestorNum = "+POut.Long(taskAncestorNum);
			Db.NonQ(command);
		}

				/*
				command="DROP TABLE IF EXISTS taskancestor";
				Db.NonQ(command);
				command=@"CREATE TABLE taskancestor (
					TaskAncestorNum bigint NOT NULL auto_increment,
					TaskNum bigint NOT NULL,
					TaskListNum bigint NOT NULL,
					PRIMARY KEY (TaskAncestorNum),
					INDEX(?)
					) DEFAULT CHARSET=utf8";
				*/

	}
}