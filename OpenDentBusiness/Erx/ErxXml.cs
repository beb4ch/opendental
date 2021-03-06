﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	public class ErxXml {

		public static string NewCropPartnerName {
			get {
				string newCropName=PrefC.GetString(PrefName.NewCropName);
				if(newCropName=="") { //Resellers use this field to send different credentials. Thus, if blank, then send OD credentials.
					return "OpenDental";
				}
				return PrefC.GetString(PrefName.NewCropPartnerName);//Reseller.
			}
		}

		public static string NewCropAccountName {
			get {
				string newCropName=PrefC.GetString(PrefName.NewCropName);
				if(newCropName=="") { //Resellers use this field to send different credentials. Thus, if blank, then send OD credentials.
#if DEBUG
					return CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
#else
					return CodeBase.MiscUtils.Decrypt("HumacKlUtM1MLCHsZY/PH86W10AY5u2bukFp15lEKhT6zD/aa9nG//zYzbYgpH8+");//Assigned by NewCrop. Used globally for all customers.
#endif
				}
				return newCropName;//Reseller.
			}
		}

		public static string NewCropAccountPasssword {
			get {
				string newCropName=PrefC.GetString(PrefName.NewCropName);
				if(newCropName=="") { //Resellers use this field to send different credentials. Thus, if blank, then send OD credentials.
#if DEBUG
					return CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
#else
					return CodeBase.MiscUtils.Decrypt("I0Itlo5F3ZOYUSwMKpgbY5X6++XpUetMvrqj0vVB7bKzYwJlWtsLiFFgpMBplLaH");//Assigned by NewCrop. Used globally for all customers.
#endif
				}
				return PrefC.GetString(PrefName.NewCropPassword);//Reseller.
			}
		}

		public static string NewCropProductName {
			get { return "OpenDental"; }
		}

		public static string NewCropProductVersion {
			get { return Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString(); }
		}

		///<summary>Only called from Chart for now.  No validation is performed here.  Validate before calling.  There are many validtion checks, including the NPI must be exactly 10 digits.</summary>
		public static string BuildClickThroughXml(Provider prov,Employee emp,Patient pat) {
			NCScript ncScript=new NCScript();
			ncScript.Credentials=new CredentialsType();
			ncScript.Credentials.partnerName=NewCropPartnerName;
			ncScript.Credentials.name=NewCropAccountName;
			ncScript.Credentials.password=NewCropAccountPasssword;
			ncScript.Credentials.productName=NewCropProductName;
			ncScript.Credentials.productVersion=NewCropProductVersion;
			ncScript.UserRole=new UserRoleType();
			if(emp==null) {
				ncScript.UserRole.user=UserType.LicensedPrescriber;
				ncScript.UserRole.role=RoleType.doctor;
			}
			else {
				ncScript.UserRole.user=UserType.Staff;
				ncScript.UserRole.role=RoleType.nurse;
			}
			ncScript.Destination=new DestinationType();
			ncScript.Destination.requestedPage=RequestedPageType.compose;//This is the tab that the user will want 90% of the time.
			string practiceTitle=PrefC.GetString(PrefName.PracticeTitle);//May be blank.
			string practicePhone=PrefC.GetString(PrefName.PracticePhone);//Validated to be 10 digits within the chart.
			string practiceFax=PrefC.GetString(PrefName.PracticeFax);//Validated to be 10 digits within the chart.
			string practiceAddress=PrefC.GetString(PrefName.PracticeAddress);//Validated to exist in chart.
			string practiceAddress2=PrefC.GetString(PrefName.PracticeAddress2);//May be blank.
			string practiceCity=PrefC.GetString(PrefName.PracticeCity);//Validated to exist in chart.
			string practiceState=PrefC.GetString(PrefName.PracticeST);//Validated to be a US state code in chart.
			string practiceZip=Regex.Replace(PrefC.GetString(PrefName.PracticeZip),"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
			string practiceZip4=practiceZip.Substring(5);//Last 4 digits of zip.
			practiceZip=practiceZip.Substring(0,5);//First 5 digits of zip.
			string country="US";//Always United States for now.
			//if(CultureInfo.CurrentCulture.Name.Length>=2) {
			//  country=CultureInfo.CurrentCulture.Name.Substring(CultureInfo.CurrentCulture.Name.Length-2);
			//}
			ncScript.Account=new AccountTypeRx();
			//Each LicensedPrescriberID must be unique within an account. Since we send ProvNum for LicensedPrescriberID, each OD database must have a unique AccountID.
			ncScript.Account.ID=PrefC.GetString(PrefName.NewCropAccountId);//Customer account number then a dash then a random alpha-numeric string of 3 characters, followed by 2 digits.
			ncScript.Account.accountName=practiceTitle;//May be blank.
			ncScript.Account.siteID="1";//Always send 1.  For each AccountID/SiteID pair, a separate database will be created in NewCrop.
			ncScript.Account.AccountAddress=new AddressType();
			ncScript.Account.AccountAddress.address1=practiceAddress;//Validated to exist in chart.
			ncScript.Account.AccountAddress.address2=practiceAddress2;//May be blank.
			ncScript.Account.AccountAddress.city=practiceCity;//Validated to exist in chart.
			ncScript.Account.AccountAddress.state=practiceState;//Validated to be a US state code in chart.
			ncScript.Account.AccountAddress.zip=practiceZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
			ncScript.Account.AccountAddress.zip4=practiceZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
			ncScript.Account.AccountAddress.country=country;//Validated above.
			ncScript.Account.accountPrimaryPhoneNumber=practicePhone;//Validated to be 10 digits within the chart.
			ncScript.Account.accountPrimaryFaxNumber=practiceFax;//Validated to be 10 digits within the chart.
			ncScript.Location=new LocationType();
			if(PrefC.GetBool(PrefName.EasyNoClinics) || pat.ClinicNum==0) { //No clinics.
				ncScript.Location.ID="0";//Always 0, since clinicnums must be >= 1, will never overlap with a clinic if the office turns clinics on after first use.
				ncScript.Location.locationName=practiceTitle;//May be blank.
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=practiceAddress;//Validated to exist in chart.
				ncScript.Location.LocationAddress.address2=practiceAddress2;//May be blank.
				ncScript.Location.LocationAddress.city=practiceCity;//Validated to exist in chart.
				ncScript.Location.LocationAddress.state=practiceState;//Validated to be a US state code in chart.
				ncScript.Location.LocationAddress.zip=practiceZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
				ncScript.Location.LocationAddress.zip4=practiceZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
				ncScript.Location.LocationAddress.country=country;//Validated above.
				ncScript.Location.primaryPhoneNumber=practicePhone;//Validated to be 10 digits within the chart.
				ncScript.Location.primaryFaxNumber=practiceFax;//Validated to be 10 digits within the chart.
				ncScript.Location.pharmacyContactNumber=practicePhone;//Validated to be 10 digits within the chart.
			}
			else { //Using clinics.
				Clinic clinic=Clinics.GetClinic(pat.ClinicNum);
				ncScript.Location.ID=clinic.ClinicNum.ToString();//A positive integer.
				ncScript.Location.locationName=clinic.Description;//May be blank.
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=clinic.Address;//Validated to exist in chart.
				ncScript.Location.LocationAddress.address2=clinic.Address2;//May be blank.
				ncScript.Location.LocationAddress.city=clinic.City;//Validated to exist in chart.
				ncScript.Location.LocationAddress.state=clinic.State;//Validated to be a US state code in chart.
				string clinicZip=Regex.Replace(clinic.Zip,"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
				string clinicZip4=clinicZip.Substring(5);//Last 4 digits of zip.
				clinicZip=clinicZip.Substring(0,5);//First 5 digits of zip.
				ncScript.Location.LocationAddress.zip=clinicZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
				ncScript.Location.LocationAddress.zip4=clinicZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
				ncScript.Location.LocationAddress.country=country;//Validated above.
				ncScript.Location.primaryPhoneNumber=clinic.Phone;//Validated to be 10 digits within the chart.
				ncScript.Location.primaryFaxNumber=clinic.Fax;//Validated to be 10 digits within the chart.
				ncScript.Location.pharmacyContactNumber=clinic.Phone;//Validated to be 10 digits within the chart.
			}
			ncScript.LicensedPrescriber=new LicensedPrescriberType();
			//Each unique provider ID sent to NewCrop will cause a billing charge.
			//Some customer databases have provider duplicates, because they have one provider record per clinic with matching NPIs.
			//We send NPI as the ID to prevent extra NewCrop charges.
			//Conversation with NewCrop:
			//Question: If one of our customers clicks through to NewCrop with 2 different LicensedPrescriber.ID values, 
			//          but with the same provider name and NPI, will Open Dental be billed twice or just one time for the NPI used?
			//Answer:   "They would be billed twice. The IDs you send us should always be maintained and unique. 
			//          Users are always identified by LicensedPrescriber ID, since their name or credentials could potentially change."
			ncScript.LicensedPrescriber.ID=prov.NationalProvID;
			//UPIN is obsolete
			ncScript.LicensedPrescriber.LicensedPrescriberName=new PersonNameType();
			ncScript.LicensedPrescriber.LicensedPrescriberName.last=prov.LName.Trim();//Cannot be blank.
			ncScript.LicensedPrescriber.LicensedPrescriberName.first=prov.FName.Trim();//Cannot be blank.
			ncScript.LicensedPrescriber.LicensedPrescriberName.middle=prov.MI;//May be blank.
			ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.DDS;//There is no blank or none option, so we have to pick a default value. DDS=0, so would be default anyway.
			string[] suffixes=prov.Suffix.ToUpper().Split(' ','.');
			for(int i=0;i<suffixes.Length;i++) {
				if(suffixes[i]=="I") {
					ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.I;
					break;
				}
				else if(suffixes[i]=="II") {
					ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.II;
					break;
				}
				else if(suffixes[i]=="III") {
					ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.III;
					break;
				}
				else if(suffixes[i]=="JR") {
					ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.Jr;
					break;
				}
				else if(suffixes[i]=="SR") {
					ncScript.LicensedPrescriber.LicensedPrescriberName.suffix=PersonNameSuffix.Sr;
					break;
				}
			}
			if(prov.DEANum.ToLower()=="none") {
				ncScript.LicensedPrescriber.dea="NONE";
			}
			else {
				ncScript.LicensedPrescriber.dea=prov.DEANum;
			}
			ncScript.LicensedPrescriber.licenseState=prov.StateWhereLicensed;//Validated to be a US state code in the chart.
			ncScript.LicensedPrescriber.licenseNumber=prov.StateLicense;//Validated to exist in chart.
			ncScript.LicensedPrescriber.npi=prov.NationalProvID;//Validated to be 10 digits in chart.
			//ncScript.LicensedPrescriber.freeformCredentials=;//This is where DDS and DMD should go, but we don't support this yet. Probably not necessary anyway.
			if(emp!=null) {
				ncScript.Staff=new StaffType();
				ncScript.Staff.ID="emp"+emp.EmployeeNum.ToString();//A positive integer. Returned in the ExternalUserID field when retreiving prescriptions from NewCrop. Also, provider ID is returned in the same field if a provider created the prescription, so that we can create a distintion between employee IDs and provider IDs.
				ncScript.Staff.StaffName=new PersonNameType();
				ncScript.Staff.StaffName.first=emp.FName;//First name or last name will not be blank. Validated in Chart.
				ncScript.Staff.StaffName.last=emp.LName;//First name or last name will not be blank. Validated in Chart.
				ncScript.Staff.StaffName.middle=emp.MiddleI;//May be blank.
			}
			ncScript.Patient=new PatientType();
			ncScript.Patient.ID=pat.PatNum.ToString();//A positive integer.
			ncScript.Patient.PatientName=new PersonNameType();
			ncScript.Patient.PatientName.last=pat.LName;//Validated to exist in Patient Edit window.
			ncScript.Patient.PatientName.first=pat.FName;//May be blank.
			ncScript.Patient.PatientName.middle=pat.MiddleI;//May be blank.
			ncScript.Patient.medicalRecordNumber=pat.PatNum.ToString();//A positive integer.
			//NewCrop specifically requested that we do not send SSN.
			//ncScript.Patient.socialSecurityNumber=Regex.Replace(pat.SSN,"[^0-9]*","");//Removes all non-numerical characters.
			ncScript.Patient.PatientAddress=new AddressOptionalType();
			ncScript.Patient.PatientAddress.address1=pat.Address;//May be blank.
			ncScript.Patient.PatientAddress.address2=pat.Address2;//May be blank.
			ncScript.Patient.PatientAddress.city=pat.City;//May be blank.
			ncScript.Patient.PatientAddress.state=pat.State;//May be blank. Validated in chart to be blank or to be a valid US state code.
			ncScript.Patient.PatientAddress.zip=pat.Zip;//May be blank.
			ncScript.Patient.PatientAddress.country=country;//Validated above.
			ncScript.Patient.PatientContact=new ContactType();
			ncScript.Patient.PatientContact.homeTelephone=pat.HmPhone;//May be blank. Does not need to be 10 digits.
			ncScript.Patient.PatientCharacteristics=new PatientCharacteristicsType();
			ncScript.Patient.PatientCharacteristics.dob=pat.Birthdate.ToString("yyyyMMdd");//DOB must be in CCYYMMDD format.
			if(pat.Gender==PatientGender.Male) {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.M;
			}
			else if(pat.Gender==PatientGender.Female) {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.F;
			}
			else {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.U;
			}
			ncScript.Patient.PatientCharacteristics.genderSpecified=true;
			//NewCrop programmer's comments regarding other fields we are not currently using (these fields are sent back when fetching prescriptions in the Chart):
			//ExternalPrescriptionId = your unique identifier for the prescription, only to be used if you are generating the prescription on your own UI.
			//	This is referenced by NewCrop, and cannot be populated with any other value. 
			//EncounterIdentifier = unique ID for the patient visit (e.g. John Doe, 11/11/2013).
			//	This is used by NewCrop for reporting events against a visit, but otherwise does not impact the session. 
			//EpisodeIdentifier = unique ID for the patient’s issue (e.g. John Doe’s broken leg) which may include multiple visits.
			//	Currently not used by NewCrop except for being echoed back; it is possible this functionality would be expanded in the future based on its intent as noted. 
			//ExternalSource = a codified field noting the origin of the prescription. This may not be used.
			//Serialize
			MemoryStream memoryStream=new MemoryStream();
			XmlSerializer xmlSerializer=new XmlSerializer(typeof(NCScript));
			xmlSerializer.Serialize(memoryStream,ncScript);
			byte[] memoryStreamInBytes=memoryStream.ToArray();
			return Encoding.UTF8.GetString(memoryStreamInBytes,0,memoryStreamInBytes.Length);
		}

	}
}
