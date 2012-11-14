package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Commlog {
		/** Primary key. */
		public int CommlogNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date and time of entry */
		public String CommDateTime;
		/** FK to definition.DefNum. This will be 0 if IsStatementSent.  Used to be an enumeration in previous versions. */
		public int CommType;
		/** Note for this commlog entry. */
		public String Note;
		/** Enum:CommItemMode Phone, email, etc. */
		public CommItemMode Mode_;
		/** Enum:CommSentOrReceived Neither=0,Sent=1,Received=2. */
		public CommSentOrReceived SentOrReceived;
		/** FK to userod.UserNum. */
		public int UserNum;
		/** Signature.  For details, see procedurelog.Signature. */
		public String Signature;
		/** True if signed using the Topaz signature pad, false otherwise. */
		public boolean SigIsTopaz;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public String DateTStamp;
		/** Date and time when commlog ended.  Mainly for internal use. */
		public String DateTimeEnd;

		/** Deep copy of object. */
		public Commlog Copy() {
			Commlog commlog=new Commlog();
			commlog.CommlogNum=this.CommlogNum;
			commlog.PatNum=this.PatNum;
			commlog.CommDateTime=this.CommDateTime;
			commlog.CommType=this.CommType;
			commlog.Note=this.Note;
			commlog.Mode_=this.Mode_;
			commlog.SentOrReceived=this.SentOrReceived;
			commlog.UserNum=this.UserNum;
			commlog.Signature=this.Signature;
			commlog.SigIsTopaz=this.SigIsTopaz;
			commlog.DateTStamp=this.DateTStamp;
			commlog.DateTimeEnd=this.DateTimeEnd;
			return commlog;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Commlog>");
			sb.append("<CommlogNum>").append(CommlogNum).append("</CommlogNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<CommDateTime>").append(Serializing.EscapeForXml(CommDateTime)).append("</CommDateTime>");
			sb.append("<CommType>").append(CommType).append("</CommType>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Mode_>").append(Mode_.ordinal()).append("</Mode_>");
			sb.append("<SentOrReceived>").append(SentOrReceived.ordinal()).append("</SentOrReceived>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<Signature>").append(Serializing.EscapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<DateTStamp>").append(Serializing.EscapeForXml(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateTimeEnd>").append(Serializing.EscapeForXml(DateTimeEnd)).append("</DateTimeEnd>");
			sb.append("</Commlog>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CommlogNum=Integer.valueOf(doc.getElementsByTagName("CommlogNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				CommDateTime=doc.getElementsByTagName("CommDateTime").item(0).getFirstChild().getNodeValue();
				CommType=Integer.valueOf(doc.getElementsByTagName("CommType").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				Mode_=CommItemMode.values()[Integer.valueOf(doc.getElementsByTagName("Mode_").item(0).getFirstChild().getNodeValue())];
				SentOrReceived=CommSentOrReceived.values()[Integer.valueOf(doc.getElementsByTagName("SentOrReceived").item(0).getFirstChild().getNodeValue())];
				UserNum=Integer.valueOf(doc.getElementsByTagName("UserNum").item(0).getFirstChild().getNodeValue());
				Signature=doc.getElementsByTagName("Signature").item(0).getFirstChild().getNodeValue();
				SigIsTopaz=(doc.getElementsByTagName("SigIsTopaz").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateTStamp=doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue();
				DateTimeEnd=doc.getElementsByTagName("DateTimeEnd").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum CommItemMode {
			/** 0-  */
			None,
			/** 1-  */
			Email,
			/** 2 */
			Mail,
			/** 3 */
			Phone,
			/** 4 */
			InPerson,
			/** 5 */
			Text
		}

		/** 0=neither, 1=sent, 2=received. */
		public enum CommSentOrReceived {
			/** 0 */
			Neither,
			/** 1 */
			Sent,
			/** 2 */
			Received
		}


}