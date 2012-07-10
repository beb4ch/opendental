﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>There is no field for MessageStructureHL7 (ADT_A01), because that will be inferred. Defined in HL7 specs, section 2.16.3.</summary>
	[Serializable]
	public class HL7DefMessage:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefMessageNum;
		///<summary>FK to HL7Def.HL7DefNum</summary>
		public long HL7DefNum;
		///<summary>Stored in db as string, but used in OD as enum MessageTypeHL7. Example: ADT</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public MessageTypeHL7 MessageType;
		///<summary>Stored in db as string, but used in OD as enum EventTypeHL7. Example: A04, which is only used iwth ADT/ACK.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public EventTypeHL7 EventType;
		///<summary>Enum:InOutHL7 Incoming, Outgoing</summary>
		public InOutHL7 InOrOut;
		///<summary>This is mostly for outgoing, since incoming can have extra unexpected segments and since we don't usually care about order of incoming.</summary>
		public int ItemOrder;
		///<summary>text</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Note;

		///<Summary>List of segments associated with this hierarchical definition.  Use items in this list to get to items lower in the hierarchy.</Summary>
		[CrudColumn(IsNotDbColumn=true)]
		[XmlIgnore]
		public List<HL7DefSegment> hl7DefSegments;

		///<summary></summary>
		public HL7DefMessage Clone() {
			return (HL7DefMessage)this.MemberwiseClone();
		}

	}

	///<summary></summary>
	public enum MessageTypeHL7 {
		///<summary>This should never happen.</summary>
		Unknown,
		///<summary>Demographics - A01,A04,A08,A28,A31</summary>
		ADT,
		///<summary>Scheduling - S12,S13,S14,S15,S22</summary>
		SIU,
		///<summary>Detailed Financial Transaction - P03</summary>
		DFT,
		///<summary>Unsolicited Vaccination Record Update - V04</summary>
		VXU,
		///<summary>Unsolicited Observation Message - R01</summary>
		ORU
	}

	public enum EventTypeHL7 {
		///<summary>0 - Only used with ADT/ACK.</summary>
		A04,
		///<summary>1 - Only used with SUI/ACK.</summary>
		S12
	}

	public enum InOutHL7 {
		///<summary>0</summary>
		Incoming,
		///<summary>1</summary>
		Outgoing
	}

}
