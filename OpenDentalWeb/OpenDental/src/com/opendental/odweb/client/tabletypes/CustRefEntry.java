package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class CustRefEntry {
		/** Primary key. */
		public int CustRefEntryNum;
		/** FK to patient.PatNum.  The customer seeking a reference. */
		public int PatNumCust;
		/** FK to patient.PatNum.  The chosen reference.  This is the customer who was given as a reference to the new customer. */
		public int PatNumRef;
		/** Date the reference was chosen. */
		public String DateEntry;
		/** Notes specific to this particular reference entry, mostly for a special reference situation. */
		public String Note;

		/** Deep copy of object. */
		public CustRefEntry Copy() {
			CustRefEntry custrefentry=new CustRefEntry();
			custrefentry.CustRefEntryNum=this.CustRefEntryNum;
			custrefentry.PatNumCust=this.PatNumCust;
			custrefentry.PatNumRef=this.PatNumRef;
			custrefentry.DateEntry=this.DateEntry;
			custrefentry.Note=this.Note;
			return custrefentry;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CustRefEntry>");
			sb.append("<CustRefEntryNum>").append(CustRefEntryNum).append("</CustRefEntryNum>");
			sb.append("<PatNumCust>").append(PatNumCust).append("</PatNumCust>");
			sb.append("<PatNumRef>").append(PatNumRef).append("</PatNumRef>");
			sb.append("<DateEntry>").append(Serializing.EscapeForXml(DateEntry)).append("</DateEntry>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</CustRefEntry>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CustRefEntryNum=Integer.valueOf(doc.getElementsByTagName("CustRefEntryNum").item(0).getFirstChild().getNodeValue());
				PatNumCust=Integer.valueOf(doc.getElementsByTagName("PatNumCust").item(0).getFirstChild().getNodeValue());
				PatNumRef=Integer.valueOf(doc.getElementsByTagName("PatNumRef").item(0).getFirstChild().getNodeValue());
				DateEntry=doc.getElementsByTagName("DateEntry").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}