package com.opendental.opendentbusiness.remoting;

import java.util.ArrayList;

import com.google.gwt.core.client.Scheduler;
import com.google.gwt.core.client.Scheduler.RepeatingCommand;
import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.Element;
import com.google.gwt.xml.client.Node;
import com.google.gwt.xml.client.NodeList;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.opendentbusiness.data.*;
import com.opendental.opendentbusiness.tabletypes.*;

/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes made will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial(). */
public class Serializing {

	/** Escapes common characters used in XML. */
	public static String escapeForXml(String myString) {
		StringBuilder strBuild=new StringBuilder();
		int length=myString.length();
		for(int i=0;i<length;i++) {
			String character=myString.substring(i,i+1);
			if(character.equals("<")) {
				strBuild.append("&lt;");
				continue;
			}
			else if(character.equals(">")) {
				strBuild.append("&gt;");
				continue;
			}
			else if(character.equals("\"")) {
				strBuild.append("&quot;");
				continue;
			}
			else if(character.equals("\'")) {
				strBuild.append("&#039;");
				continue;
			}
			else if(character.equals("&")) {
				strBuild.append("&amp;");
				continue;
			}
			strBuild.append(character);
		}
		return strBuild.toString();
	}

	/** Escapes common characters used in URLs. */
	public static String escapeForURL(String myString) {
		StringBuilder strBuild=new StringBuilder();
		int length=myString.length();
		for(int i=0;i<length;i++) {
			String character=myString.substring(i,i+1);
			if(character.equals("<")) {
				strBuild.append("%3C");
				continue;
			}
			else if(character.equals(">")) {
				strBuild.append("%3E");
				continue;
			}
			else if(character.equals("&")) {
				strBuild.append("%26");
				continue;
			}
			strBuild.append(character);
		}
		return strBuild.toString();
	}

	/** Loops through all the known objects and calls the corresponding classes serialize method.
	 * @param obj Can be any type of object.  Error will occur if the type hasn't been implemented yet. 
	 * @throws Exception Throws exception if type is not yet supported. */
	public static String getSerializedObject(Object obj) throws Exception {
		String result;
		//Figure out what type of object we're dealing with and return the serialized form.
		String qualifiedName=obj.getClass().getName();//Ex: ArrayList = "java.util.ArrayList"
		//Primitives--------------------------------------------------------------------------------------------------------
		if(qualifiedName.equals("Z") || qualifiedName.equals("java.lang.Boolean")) {//boolean  "Z"
			result=(Boolean)obj?"1":"0";
			return "<bool>"+result+"</bool>";
		}
		if(qualifiedName.equals("B") || qualifiedName.equals("java.lang.Byte")) {//byte  "B"
			return "<byte>"+(Byte)obj+"</byte>";
		}
		if(qualifiedName.equals("C") || qualifiedName.equals("java.lang.Character")) {//char  "C"
			return "<char>"+(Character)obj+"</char>";
		}
		if(qualifiedName.equals("S") || qualifiedName.equals("java.lang.Short")) {//short  "S"
			return "<short>"+(Short)obj+"</short>";
		}
		if(qualifiedName.equals("I") || qualifiedName.equals("java.lang.Integer")) {//int  "I"
			return "<int>"+(Integer)obj+"</int>";
		}
		if(qualifiedName.equals("J") || qualifiedName.equals("java.lang.Long")) {//long  "J"
			//return "<long>"+(Long)obj+"</long>";
		}
		if(qualifiedName.equals("F") || qualifiedName.equals("java.lang.Float")) {//float  "F"
			return "<float>"+(Float)obj+"</float>";
		}
		if(qualifiedName.equals("D") || qualifiedName.equals("java.lang.Double")) {//double  "D"
			return "<double>"+(Double)obj+"</double>";
		}
		if(qualifiedName.equals("java.lang.String")) {//String  "java.lang.String"
			return "<string>"+(String)obj+"</string>";
		}
		if(qualifiedName.equals("java.util.Date")) {//Date  "java.util.Date"
			// TODO Enhance serializer to handle Date objects.
			return "<DateTime>0001-01-01</DateTime>";
		}
		//Arrays------------------------------------------------------------------------------------------------------------
		//Multidimensional arrays have equal number of brackets. Ex: Account[][] = [[L...
		//Object[]  "[Lcom.opendental.odweb.client.tabletypes.Account;" from Account[]
		//int[]     "[I"
		//String[]  "[Ljava.lang.String;"
		//Open Dental Objects-----------------------------------------------------------------------------------------------
		if(qualifiedName.equals("com.opendental.odweb.client.tabletypes.Appointment")) {
			return ((Appointment)obj).serialize();
		}
		if(qualifiedName.equals("com.opendental.odweb.client.tabletypes.Patient")) {
			return ((Patient)obj).serialize();
		}
		if(qualifiedName.equals("com.opendental.odweb.client.tabletypes.Pref")) {
			return ((Pref)obj).serialize();
		}
		if(qualifiedName.equals("com.opendental.odweb.client.tabletypes.Userod")) {
			return ((Userod)obj).serialize();
		}
		throw new Exception("getSerializedObject, unsupported type: "+qualifiedName);
	}

/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial() */

	/** Loops through all the known objects and calls the corresponding classes deserialize method.
	 * @param xml The serialized response from the server.  Handles DtoExceptions.
	 * @param deserializeCallback After deserializing is complete, the onComplete method of this callback will be called.
	 * @throws Exception Throws exception if type is not yet supported or if a DtoException was returned. */
	public static void getDeserializedObject(String xml,DeserializeCallbackResult deserializeCallback) throws Exception {
		Document doc=XMLParser.parse(xml);
		//XMLParser.removeWhitespace(doc);
		Element element=doc.getDocumentElement();
		if(element==null) {
			throw new Exception("getDeserializedObject, the response from server was not valid XML.");
		}
		//Figure out the response type.  Response examples: <long>4</long> OR <DtoException><msg>Error</msg></DtoException>
		String type=element.getNodeName();
		//Void method calls will simply return an empty void node:  <void />
		if(type.equals("void")) {
			deserializeCallback.onComplete(null);
			return;
		}
		if(type.equals("DtoException")) {//Check for exceptions first.
			//Read the "msg" node and throw an exception with that error message.
			throw new Exception(doc.getElementsByTagName("msg").item(0).getFirstChild().getNodeValue());
		}
		//Primitives-------------------------------------------------------------------------------------------------------
		if(type.equals("boolean")) {
			deserializeCallback.onComplete(element.getFirstChild().getNodeValue()=="0"?false:true);
			return;
		}
		if(type.equals("byte")) {
			deserializeCallback.onComplete(Byte.parseByte(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("char")) {
			deserializeCallback.onComplete(element.getFirstChild().getNodeValue().charAt(0));
			return;
		}
		if(type.equals("short")) {
			deserializeCallback.onComplete(Short.parseShort(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("int")) {
			deserializeCallback.onComplete(Integer.parseInt(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("long")) {
			deserializeCallback.onComplete(Long.parseLong(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("float")) {
			deserializeCallback.onComplete(Float.parseFloat(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("double")) {
			deserializeCallback.onComplete(Double.parseDouble(element.getFirstChild().getNodeValue()));
			return;
		}
		if(type.equals("String")) {
			deserializeCallback.onComplete(element.getFirstChild().getNodeValue());
			return;
		}
		if(type.equals("DataTable")) {
			deserializeDataTable(element,deserializeCallback);
			return;
		}
		if(type.equals("List")) {
			deserializeList(element,deserializeCallback);
			return;
		}
		//Open Dental object-------------------------------------------------------------------------------------------------
		Object result=deserializeOpenDentalObject(type,doc);
		if(result==null) {
			throw new Exception("getDeserializedObject, unsupported type: "+type);
		}
		deserializeCallback.onComplete(result);
	}

	/** Pass in the type and just the xml for that object.  Returns null if no match found. */
	private static Object deserializeOpenDentalObject(String type,Document doc) throws Exception {
		if(type.equals("Appointment")) {
			Appointment appointment=new Appointment();
			appointment.deserialize(doc);
			return appointment;
		}
		if(type.equals("Patient")) {
			Patient patient=new Patient();
			patient.deserialize(doc);
			return patient;
		}
		if(type.equals("Pref")) {
			Pref pref=new Pref();
			pref.deserialize(doc);
			return pref;
		}
		if(type.equals("Userod")) {
			Userod userod=new Userod();
			userod.deserialize(doc);
			return userod;
		}
		return null;
	}

/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial() */

	/** Pass in the entire xml response and this method will return a deserialized ArrayList.
	 * @throws Exception Throws exception if the list cannot be deserialized. */
	private static void deserializeList(Node node,final DeserializeCallbackResult deserializeCallback) throws Exception {
		//Create an array list of objects
		final ArrayList<Object> arrayList=new ArrayList<Object>();
		final ArrayList<Node> nodeListObjects=getChildNodesFiltered(node);
		//Loop through the entire list of children and create an object for each and then add it to the list.
		RepeatingCommand rc=new RepeatingCommand() {
			private int i=0;
			public boolean execute() {
				if(i<nodeListObjects.size()) {
					//Create another deserialize callback so we know to add that object to the array list.
					try {
						getDeserializedObject(nodeListObjects.get(i).toString(),new DeserializeCallbackResult() {
							public void onComplete(Object obj) {
								arrayList.add(obj);
							}
							public void onError(String error) {
								//Not sure how to have the error message passed on.  Do nothing for now.
							}
						});
					}
					catch (Exception e) {
						//No idea what to do here.
					}
					i++;
					return true;
				}
				if(arrayList.size()!=nodeListObjects.size()) {//This might not be necessary.
					return true;//Keep waiting for the list to fill up.
				}
				deserializeCallback.onComplete(arrayList);
				return false;
			}
		};
		Scheduler.get().scheduleIncremental(rc);
	}

	/** Pass in a node from the response and this method will digest the entire XML and return a deserialized DataTable. */
	private static void deserializeDataTable(Node node,final DeserializeCallbackResult deserializeCallback) {
		ArrayList<Node> nodeListAll=getChildNodesFiltered(node);
		Node nodeName=nodeListAll.get(0);//Name node always exists.
		String tableName="";
		if(nodeName.getChildNodes().getLength()>0) {
			tableName=nodeName.getChildNodes().item(0).getNodeValue();
		}
		Node nodeCols=nodeListAll.get(1);//Cols node always exists.
		ArrayList<Node> nodeListCols=getChildNodesFiltered(nodeCols);
		if(nodeListCols==null) {
			deserializeCallback.onComplete(new DataTable());//Should not happen. This could only happen if an empty data table is passed back (no columns).
			return;
		}
		ArrayList <DataColumn> columns=new ArrayList<DataColumn>();
		for(int i=0;i<nodeListCols.size();i++) {
			Node nodeCol=nodeListCols.get(i);//At this point, Col node always exists.
			String nodeColVal=nodeCol.getChildNodes().item(0).getNodeValue();//Should never be null or empty string.
			columns.add(new DataColumn(nodeColVal));
		}
		Node nodeRows=nodeListAll.get(2);//Cells node always exists.
		final ArrayList<Node> nodeListRows=getChildNodesFiltered(nodeRows);
		if(nodeListRows==null) {//This happens if the query contains no results.
			deserializeCallback.onComplete(new DataTable(tableName,columns));
			return;
		}
		final DataTable table=new DataTable(tableName,columns);
		//Deserializing the rows can take a while and cause browsers to show an unresponsive script warning message.
		//Using IncrementalCommand will return the main thread back to the browser and then call execute again so that the browser doesn't "freeze".
		RepeatingCommand rc=new RepeatingCommand() {
			private int i=0;
			public boolean execute() {
				if(i<nodeListRows.size()) {
					table.Rows.add(new DataRow());
					Node nodeRow=nodeListRows.get(i);//At this point, y node always exists.
					ArrayList<Node> nodeListCells=getChildNodesFiltered(nodeRow);//Should never be null.
					for(int j=0;j<nodeListCells.size();j++) {
						Node nodeCell=nodeListCells.get(j);//At this point, x node always exists.
						String cellVal="";
						if(nodeCell.getChildNodes().getLength()>0) {
							cellVal=nodeCell.getChildNodes().item(0).getNodeValue();
						}
						table.Rows.get(i).addCell(cellVal);
					}
					i++;
					return true;
				}
				deserializeCallback.onComplete(table);
				return false;
			}
		};
		Scheduler.get().scheduleIncremental(rc);
	}

	/** Children of the given node minus nodes which are text nodes.  Such as tabs or new lines. */
	private static ArrayList<Node> getChildNodesFiltered(Node node) {
		//Create an empty node list to fill.
		ArrayList<Node> result=new ArrayList<Node>();
		NodeList childNodes=node.getChildNodes();
		if(childNodes==null) {
			return result;
		}
		for(int i=0;i<childNodes.getLength();i++) {
			Node childNode=childNodes.item(i);
			if(childNode.getNodeType()==Node.TEXT_NODE) {
				continue;
			}
			//Add the node to the node list.
			result.add(childNode);
		}
		return result;//Return the filtered node list.
	}

	/** Pass in the xml string parsed into a Document and the desired tagname to attempt to get the value.
	 * Returns the node value or null if node is not included in the Document. */
	public static String getXmlNodeValue(Document doc,String tagname) {
		NodeList list=doc.getElementsByTagName(tagname);
		if(list!=null && list.getLength()>0) {
			Node node=list.item(0).getFirstChild();
			if(node!=null) {
				//We trim so that empty nodes to not insert "/n" into string variables.
				return node.getNodeValue().trim();
			}
		}
		return null;
	}
	
	/** The sole purpose of this interface is to return a result to the Db class after the repeating command has finished.  The Db class should be the only class listening to this callback. */
	public interface DeserializeCallbackResult {
		void onComplete(Object obj);
		void onError(String error);
	}

}

