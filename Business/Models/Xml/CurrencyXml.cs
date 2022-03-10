﻿/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
	[XmlRoot(ElementName = "Currency")]
	public class CurrencyXml
	{
		[XmlElement(ElementName = "Unit")]
		public string Unit { get; set; }
		[XmlElement(ElementName = "Isim")]
		public string Isim { get; set; }
		[XmlElement(ElementName = "CurrencyName")]
		public string CurrencyName { get; set; }
		[XmlElement(ElementName = "ForexBuying")]
		public string ForexBuying { get; set; }
		[XmlElement(ElementName = "ForexSelling")]
		public string ForexSelling { get; set; }
		[XmlElement(ElementName = "BanknoteBuying")]
		public string BanknoteBuying { get; set; }
		[XmlElement(ElementName = "BanknoteSelling")]
		public string BanknoteSelling { get; set; }
		[XmlElement(ElementName = "CrossRateUSD")]
		public string CrossRateUSD { get; set; }
		[XmlElement(ElementName = "CrossRateOther")]
		public string CrossRateOther { get; set; }
		[XmlAttribute(AttributeName = "CrossOrder")]
		public string CrossOrder { get; set; }
		[XmlAttribute(AttributeName = "Kod")]
		public string Kod { get; set; }
		[XmlAttribute(AttributeName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "Tarih_Date")]
	public class CurrencyDateXML
	{
		[XmlElement(ElementName = "Currency")]
		public List<CurrencyXml> Currency { get; set; }
		[XmlAttribute(AttributeName = "Tarih")]
		public string Tarih { get; set; }
		[XmlAttribute(AttributeName = "Date")]
		public string Date { get; set; }
		[XmlAttribute(AttributeName = "Bulten_No")]
		public string Bulten_No { get; set; }
	}

}
