using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	//The other file was simply getting too big.  It was bogging down VS speed.
	///<summary></summary>
	public partial class ClassConvertDatabase{
		private System.Version LatestVersion=new Version("6.0.0.0");//This value must be changed when a new conversion is to be triggered.
		
		private void To5_9_1() {
			if(FromVersion<new Version("5.9.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DELETE FROM preference WHERE PrefName='RxOrientVert'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustRight'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustDown'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxGeneric'";
					General.NonQ(command);
					command="ALTER TABLE rxdef ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE rxpat ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);
					command="UPDATE rxdef SET IsControlled = 1";
					General.NonQ(command);
					//UAppoint Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'UAppoint', "
						+"'UAppoint from www.uappoint.com', "
						+"'0', "
						+"'"+POut.PString(@"https://s0.uappoint.com/Sync")+"', "
						+"'', "
						+"'')";
					int programNum=General.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'Username', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'Password', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'WorkstationName', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'IntervalSeconds', "
						+"'15')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'DateTimeLastUploaded', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'SynchStatus', "
						+"'')";
					General.NonQ(command);
					command="ALTER TABLE patient ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE patient SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE provider ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE provider SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE appointment SET DateTStamp=NOW()";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS deletedobject";
					General.NonQ(command);
					command=@"CREATE TABLE deletedobject (
						DeletedObjectNum int NOT NULL auto_increment,
						ObjectNum int NOT NULL,
						ObjectType int NOT NULL,
						DateTStamp TimeStamp,
						PRIMARY KEY (DeletedObjectNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE schedule SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE operatory ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE operatory SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE recall ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE recall SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE procedurecode ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE procedurecode SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE insplan ADD IsHidden tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE carrier ADD IsHidden tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE insplan ADD INDEX (CarrierNum)";
					try {
						General.NonQ(command);
					}
					catch {
					}
					try {
						//this functionality is also duplicated in SheetUtil.GetImagePath, but we try very hard not to use external routines during conversions.
						if(!PrefC.UsingAtoZfolder) {
							throw new ApplicationException("Must be using AtoZ folders.");
						}
						string imagePath=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"SheetImages");
						if(!Directory.Exists(imagePath)) {
							Directory.CreateDirectory(imagePath);
						}
						Properties.Resources.Med_History.Save(ODFileUtils.CombinePaths(imagePath,"Med History.gif"));
						Properties.Resources.Patient_Info.Save(ODFileUtils.CombinePaths(imagePath,"Patient Info.gif"));
					}
					catch{
					}
					command="DELETE FROM preference WHERE PrefName='ShowNotesInAccount'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AllowSettingProcsComplete','0','')";
					General.NonQ(command);
				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '5.9.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_0_0();
		}

		private void To6_0_0() {
			if(FromVersion<new Version("6.0.0.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject','Continuing Care Reminder','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallStatusMailed','0','FK to definition.DefNum')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallStatusEmailed','0','FK to definition.DefNum')";
					General.NonQ(command);
					command="ALTER TABLE toothinitial ADD DrawingSegment text";
					General.NonQ(command);
					command="ALTER TABLE toothinitial ADD ColorDraw int NOT NULL";
					General.NonQ(command);
					//Dolphin bridge------------------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'Dolphin', "
						+"'Dolphin from dolphinimaging.com', "
						+"'0', "
						+"'"+POut.PString(@"C:\Dolphin\")+"', "
						+"'', "
						+"'The path is to a folder rather than to a specific file.  Filename property refers to the input filename used to transer data.')";
					int programNum=General.NonQ(command,true);//we now have a ProgramNum to work with
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
						+"'0')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Filename', "
						+"'"+POut.PString(@"C:\Dolphin\Import\Import.txt")+"')";
					General.NonQ(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
						+"VALUES ("
						+"'"+POut.PInt(programNum)+"', "
						+"'"+POut.PInt((int)ToolBarsAvail.ChartModule)+"', "
						+"'Dolphin')";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeArrived DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeArrived = '0001-01-01'";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeSeated DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeSeated = '0001-01-01'";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeDismissed DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeDismissed = '0001-01-01'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeArrivedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeSeatedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeDismissedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ApptModuleRefreshesEveryMinute','1','Keeps the waiting room indicator times current.')";
					General.NonQ(command);
					command="ALTER TABLE recall ADD RecallTypeNum int NOT NULL";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS recalltype";
					General.NonQ(command);
					command=@"CREATE TABLE recalltype (
						RecallTypeNum int NOT NULL auto_increment,
						Description varchar(255),
						DefaultInterval int NOT NULL,
						TimePattern varchar(255),
						Procedures varchar(255),
						PRIMARY KEY (RecallTypeNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS recalltrigger";
					General.NonQ(command);
					command=@"CREATE TABLE recalltrigger (
						RecallTriggerNum int NOT NULL auto_increment,
						RecallTypeNum int NOT NULL,
						CodeNum int NOT NULL,
						PRIMARY KEY (RecallTriggerNum),
						INDEX (CodeNum),
						INDEX (RecallTypeNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//Basic recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPattern'";
					string timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProcedures'";
					string procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(1,'Prophy',"
						+"393216,"//six months
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
					command="SELECT CodeNum FROM procedurecode WHERE SetRecall=1";
					DataTable table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++){
						command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(1,"+table.Rows[i][0].ToString()+")";
						General.NonQ(command);
					}
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialProphy','1','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					//Child recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPatternChild'";
					timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProceduresChild'";
					procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(2,'Child Prophy',"
						+"0,"
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
					//no triggers
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialChildProphy','2','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					//Perio recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPatternPerio'";
					timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProceduresPerio'";
					procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(3,'Perio',"
						+"262144,"//4 months.
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
	//todo:perio triggers:
					//command="SELECT ValueString FROM preference WHERE PrefName='RecallPerioTriggerProcs'";
					//triggers=General.GetCount(command);


					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialPerio','3','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					if(CultureInfo.CurrentCulture.Name=="en-US"){
						//4BWX-----------------------------------------------------------------------------
						timepattern="";
						procs="D0274";
						command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
							+"VALUES(4,'4BW',"
							+"16777216,"//one year.
							+"'"+timepattern+"',"
							+"'"+POut.PString(procs)+"')";
						General.NonQ(command);
						command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(4,'D0274')";
						General.NonQ(command);
						//Pano-----------------------------------------------------------------------------
						timepattern="";
						procs="D0330";
						command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
							+"VALUES(5,'Pano',"
							+"83886080,"//5 years.
							+"'"+timepattern+"',"
							+"'"+POut.PString(procs)+"')";
						General.NonQ(command);
						command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(5,'D0330')";
						General.NonQ(command);
					}
//Set existing recall objects to new types (incomplete)
					command="UPDATE recall SET RecallTypeNum=1 WHERE RecallTypeNum=0";
					General.NonQ(command);



					//Get rid of unused prefs-----------------------------------------------------------------
					command="DELETE FROM preference WHERE PrefName='RecallPattern'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProcedures'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPatternChild'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProceduresChild'";
					General.NonQ(command);
					command="ALTER TABLE procedurecode DROP SetRecall";
					General.NonQ(command);
					command="ALTER TABLE procedurecode DROP RemoveTooth";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallBW'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallFMXPanoProc'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallDisableAutoFilms'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallFMXPanoYrInterval'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallDisablePerioAlt'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPatternPerio'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProceduresPerio'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPerioTriggerProcs'";
					General.NonQ(command);
					


					







				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '6.0.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To6_0_?();
		}

		/*For 6.0:
		 * ALTER TABLE schedule ADD INDEX (EmployeeNum)
ALTER TABLE schedule ADD INDEX (ProvNum)
ALTER TABLE schedule ADD INDEX (SchedDate)*/


	}

}