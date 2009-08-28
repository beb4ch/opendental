using System;
using System.Collections;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Text;
//using System.Windows.Forms;

namespace OpenDentBusiness{
	
	/*=========================================================================================
	=================================== class PIn ===========================================*/
	///<summary>Converts strings coming in from the database into the appropriate type. "P" was originally short for Parameter because this class was written specifically to replace parameters in the mysql queries. Using strings instead of parameters is much easier to debug.  This will later be rewritten as a System.IConvertible interface on custom mysql types.  I would rather not ever depend on the mysql connector for this so that this program remains very db independent.</summary>
	public class PIn{
		///<summary></summary>
		public static bool PBool (string myString){
			return myString=="1";
		}

		///<summary></summary>
		public static byte PByte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		///<summary></summary>
		public static DateTime PDate(string myString){
			if(myString=="")
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
				//return DateTime.Parse(myString,CultureInfo.InvariantCulture);
			}
			catch{
				return DateTime.MinValue;
			}
		}

		///<summary></summary>
		public static DateTime PDateT(string myString){
			if(myString=="")
				return DateTime.MinValue;
			//if(myString=="0000-00-00 00:00:00")//useless
			//	return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		public static TimeSpan PTimeSpan(string myString) {
			if (string.IsNullOrEmpty(myString)) {
				return TimeSpan.MinValue;
			}
			try {
				return (TimeSpan.Parse(myString));
			}
			catch {
				return TimeSpan.MinValue;
			}
		}

		///<summary>If blank or invalid, returns 0. Otherwise, parses.</summary>
		public static double PDouble (string myString){
			if(myString==""){
				return 0;
			}
			else{
				try{
					return System.Convert.ToDouble(myString);
				}
				catch{
					//MessageBox.Show("Error converting "+myString+" to double");
					return 0;
				}
			}

		}

		///<summary></summary>
		public static long PInt (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToInt64(myString);
			}
		}

		///<summary></summary>
		public static int PInt32(string myString) {
			if(myString=="") {
				return 0;
			}
			else {
				return System.Convert.ToInt32(myString);
			}
		}

		///<summary></summary>
		public static short PShort(string myString) {
			if(myString == "") {
				return 0;
			}
			else {
				return System.Convert.ToInt16(myString);
			}
		}

		///<summary></summary>
		public static float PFloat(string myString){
			if(myString==""){
				return 0;
			}
			//try{
				return System.Convert.ToSingle(myString);
			//}
			//catch{
			//	return 0;
			//}
		}

		///<summary>Currently does nothing.</summary>
		public static string PString (string myString){
			return myString;
		}
		
		//<summary></summary>
		//public static string PTime (string myTime){
		//	return DateTime.Parse(myTime).ToString("HH:mm:ss");
		//}

		//<summary></summary>
		//public static string PBytes (byte[] myBytes){
		//	return Convert.ToBase64String(myBytes);
		//}

		/*
		///<summary></summary>
		public static Bitmap PBitmap(string myString){
			if(myString==""){
				return null;
			}
			byte[] rawData=Convert.FromBase64String(myString);
			MemoryStream stream=new MemoryStream(rawData);
			Bitmap image=new Bitmap(stream);
			return image;
		}*/

		///<summary></summary>
		public static Bitmap PBitmap(string myString) {
			if(myString==null || myString.Length<0x32) {//Bitmaps require a minimum length for header info.
				return null;
			}
			try {
				byte[] rawData=Convert.FromBase64String(myString);
				MemoryStream stream=new MemoryStream(rawData);
				Bitmap image=new Bitmap(stream);
				return image;
			}
			catch {
				return null;
			}
		}

		///<summary>Saves the string representation of a sound into a .wav file.  The timing of this is different than with the other "P" functions, and is only used by the export button in FormSigElementDefEdit</summary>
		public static void PSound(string sound, string filename) {
			if(!filename.EndsWith(".wav")) {
				throw new ApplicationException("Filename must end with .wav");
			}
			byte[] rawData=Convert.FromBase64String(sound);
			FileStream stream=new FileStream(filename,FileMode.Create,FileAccess.Write);
			stream.Write(rawData,0,rawData.Length);
		}

		///<summary>Some versions of MySQL return a GROUP_CONCAT as a string, and other versions return it as a byte array.  This method handles either way, making it work smoothly with different versions.</summary>
		public static string PByteArray(object obj){
			if(obj.GetType()==typeof(Byte[])){
				Byte[] bytes=(Byte[])obj;
				StringBuilder strbuild=new StringBuilder();
				for(int i=0;i<bytes.Length;i++){
					strbuild.Append((char)bytes[i]);
				}
				return strbuild.ToString();
			}
			else{//string
				return obj.ToString();
			}
		}


	}

	


}










