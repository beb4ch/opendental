using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness.DataAccess;
using System.Diagnostics;

namespace OpenDentBusiness {
	///<summary>Provides a base class for DTO classes.  A DTO class is a simple data storage object.  A DTO is the only format accepted by OpenDentBusiness.dll.</summary>
	public abstract class DataTransferObject {
		///<summary>Always passed with new web service.  Never null.</summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public DtoObject[] Params;
		
		public string Serialize(){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter writer=XmlWriter.Create(strBuild);
			XmlSerializer serializer = new XmlSerializer(this.GetType());
			serializer.Serialize(writer,this);
			writer.Close();
			return strBuild.ToString();
		}

		public static DataTransferObject Deserialize(string data) {
			StringReader strReader=new StringReader(data);
			XmlReader reader=XmlReader.Create(strReader);
			string strNodeName="";
			while(reader.Read()){
				if(reader.NodeType!=XmlNodeType.Element){
					continue;
				}
				strNodeName=reader.Name;
				break;
			}
			strReader.Close();
			reader.Close();
			Type type = Type.GetType("OpenDentBusiness." +strNodeName);
			StringReader strReader2=new StringReader(data);
			XmlReader reader2=XmlReader.Create(strReader2);
			XmlSerializer serializer = new XmlSerializer(type);
			DataTransferObject retVal=(DataTransferObject)serializer.Deserialize(reader2);
			strReader2.Close();
			reader2.Close();
			return retVal;
		}
	}

	///<summary>The username and password are internal to OD.  They are not the MySQL username and password.</summary>
	public class Credentials{
		public string Username;
		public string PassHash;
	}

	///<summary></summary>
	public class DtoGetDS:DataTransferObject{
	
	}

	///<summary></summary>
	public class DtoGetTable:DataTransferObject{
		
	}

	///<summary></summary>
	public class DtoGetTableLow:DataTransferObject {

	}

	///<summary>Gets a long.</summary>
	public class DtoGetInt:DataTransferObject{
		
	}

	///<summary>Gets an int.</summary>
	public class DtoGetInt32:DataTransferObject {

	}

	///<summary>Used when the return type is void.  It will still return 0 to ack.</summary>
	public class DtoGetVoid:DataTransferObject {
		
	}

	///<summary>Gets an object which must be serializable.  Calling code will convert object to specific type.</summary>
	public class DtoGetObject:DataTransferObject{
		///<summary>This is the "FullName" string representation of the type of object that we expect back as a result.  Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  DataTable and DataSet not allowed.</summary>
		public string ObjectType;
	}

	///<summary>Gets a simple string.</summary>
	public class DtoGetString:DataTransferObject{
		
	}

	///<summary>Gets a bool.</summary>
	public class DtoGetBool:DataTransferObject {
		
	}

	

	

	//<summary>IDorRows will be the InsertID for insert type commands.  For some other commands, it will be the rows changed, and for some commands, it will just be 0.</summary>
	//public class DtoServerAck:DataTransferObject {
	//	public int IDorRows;
	//}

	///<summary>OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally.</summary>
	public class DtoException:DataTransferObject {
		public string Message;
	}

	



}
