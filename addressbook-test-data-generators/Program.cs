using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];
            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();
            if (choice == "groups") 
            {
                
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }
            } else 
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30))
                    {
                        Nickname = TestBase.GenerateRandomString(30),
                        Company = TestBase.GenerateRandomString(30),
                        Title = TestBase.GenerateRandomString(30),
                        Address = TestBase.GenerateRandomString(50),
                        HomePhone = TestBase.GeneratePhoneNumber(),
                        MobilePhone = TestBase.GeneratePhoneNumber(),
                        WorkPhone = TestBase.GeneratePhoneNumber(),
                        Fax = TestBase.GeneratePhoneNumber(),
                        Homepage = TestBase.GenerateRandomString(30),
                        SecHomePhone = TestBase.GeneratePhoneNumber(),
                        SecAdress = TestBase.GenerateRandomString(50),
                        Notes = TestBase.GenerateRandomString(50),
                        Email = TestBase.GenerateRandomString(10) + "@" + TestBase.GenerateRandomString(10) + "." + TestBase.GenerateRandomString(4),
                        Email2 = TestBase.GenerateRandomString(10) + "@" + TestBase.GenerateRandomString(10) + "." + TestBase.GenerateRandomString(4),
                        Email3 = TestBase.GenerateRandomString(10) + "@" + TestBase.GenerateRandomString(10) + "." + TestBase.GenerateRandomString(4)
                    });
                }
            }
            
            if (format == "excel")
            {
                WriteGroupsToExcelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    if (choice == "groups")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    if (choice == "groups")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }
                    writeContactsToJsonFile(contacts, writer);
                        
                }
                else
                {
                    System.Console.Out.WriteLine("Unrecognized format" + format);
                }
                writer.Close();
            }
            
            static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
            {
                foreach (GroupData group in groups)
                {
                    writer.WriteLine(String.Format("${0},${1},${2}",
                        group.Name, group.Header, group.Footer));
                }
            }
            static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
            {
                new XmlSerializer(typeof(List<GroupData>))
                    .Serialize(writer,groups);
            }
            static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
            {
                new XmlSerializer(typeof(List<ContactData>))
                    .Serialize(writer, contacts);
            }
            static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
            }
            static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
            }
        }

         static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullpath);
            wb.SaveAs(fullpath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}
