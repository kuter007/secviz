﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Xml;

namespace KnowledgeBaseCreation
{
    public class KnowledgeBaseCreator
    {
        private string file;
        private string host;
        private string port;
        private string databaseName;
        private string username;
        private string password;

        public KnowledgeBaseCreator()
        {
            file = null;
            host = null;
            port = null;
            databaseName = null;
            username = null;
            password = null;
        }

        public void SetKnowledgeBaseFile(string fileName)
        {
            file = fileName;
        }
        public void SetHost(string inputHost)
        {
            host = inputHost;
        }
        public void SetPort(string inputPort)
        {
            port = inputPort;
        }
        public void SetDatabaseName(string inputDatabaseName)
        {
            databaseName = inputDatabaseName;
        }
        public void SetUsername(string inputUsername)
        {
            username = inputUsername;
        }
        public void SetPassword(string inputPassword)
        {
            password = inputPassword;
        }
        public void CreateTable()
        {
            string connString = "Data Source=" + host + ":" + port + "/" + databaseName + ";"
                                + "User Id=" + username + ";" + "Password=" + password + ";";
            OracleConnection conn = null;
            try
            {
                conn = DatabaseConnection.getDatabaseConnection(connString);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                string query;

                query = "create table predicate" +
                               "(" +
                                  "PredicateName varchar(100)," +
                                  "primary key (PredicateName)" +
                               ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Argument" +
                               "(" +
                                  "ArgId varchar(10)," +
                                  "ArgPos varchar(10)," +
                                  "ArgAttr varchar(100)," +
                                  "primary key (ArgId)" +
                               ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HasArgument" +
                               "(" +
                                  "PredicateName varchar(100)," +
                                  "ArgID varchar(10)," +
                                  "primary key(PredicateName, ArgID)," +
                                  "foreign key(PredicateName) references Predicate(PredicateName)," +
                                  "foreign key(ArgId) references Argument(ArgId)" +
                               ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HyperAlertType" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "primary key (HyperAlertTypeName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Fact" +
                        "(" +
                            "FactName varchar(100)," +
                            "FactType varchar(100)," +
                            "primary key (FactName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Protocol" +
                        "(" +
                            "ProtocolName varchar(100)," +
                            "primary key (ProtocolName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Implication" +
                        "(" +
                        "ImplyingName varchar(100)," +
                        "ImpliedName varchar(100)," +
                        "ImplyingArg varchar(10)," +
                        "ImpliedArg varchar(10)," +
                        //"primary key (ImplyingName, ImpliedName, ImplyingArg, ImpliedArg)," +
                        "foreign key (ImplyingName) references Predicate(PredicateName)," +
                        "foreign key (ImpliedName) references Predicate(PredicateName)," +
                        "foreign key (ImplyingArg) references Argument(ArgId)," +
                        "foreign key (ImpliedArg) references Argument(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HasFact" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "FactName varchar(100)," +
                            "primary key (HyperAlertTypeName, FactName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (FactName) references Fact(FactName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table HasProtocol" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "ProtocolName varchar(100)," +
                            "primary key (HyperAlertTypeName, ProtocolName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (ProtocolName) references Protocol(ProtocolName)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Prerequisite" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "PredicateName varchar(100)," +
                            "ArgId varchar(10)," + 
                            "ArgName varchar(100)," +
                            "primary key (HyperAlertTypeName, PredicateName, ArgId, ArgName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (PredicateName) references Predicate(PredicateName)," +
                            "foreign key (ArgId) references Argument(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                query = "create table Consequence" +
                        "(" +
                            "HyperAlertTypeName varchar(100)," +
                            "PredicateName varchar(100)," +
                            "ArgId varchar(10)," +
                            "ArgName varchar(100)," +
                            "primary key (HyperAlertTypeName, PredicateName, ArgId, ArgName)," +
                            "foreign key (HyperAlertTypeName) references HyperAlertType(HyperAlertTypeName)," +
                            "foreign key (PredicateName) references Predicate(PredicateName)," +
                            "foreign key (ArgId) references Argument(ArgId)" +
                        ")";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();

                cmd.Dispose();
                MessageBox.Show("All tables are created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void AddKnowledgeBase()
        {
            string connString = "Data Source=" + host + ":" + port + "/" + databaseName + ";"
                                + "User Id=" + username + ";" + "Password=" + password + ";";
            OracleConnection conn = null;
          
            try
            {
                conn = DatabaseConnection.getDatabaseConnection(connString);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                XmlTextReader reader = new XmlTextReader(file);
                reader.WhitespaceHandling = WhitespaceHandling .None;
                string query = null;
                string predicateName = null;
                string id = null;
                string pos = null;
                string attr = null;
                string argName = null;
                string implyingName = null;
                string impliedName = null;
                string implyingArg = null;
                string impliedArg = null;
                string hyperAlertTypeName = null;
                string factName = null;
                string factType = null;
                string protocolName = null;

                OracleDataReader rd = null;

                int implyPart = 0;

                int count = 0;

                int part = 0;

                int preOrCon = 0;

                while (reader.Read())
                {
                    if (count > 1000)
                    {
                        count = 0;
                        cmd.Dispose();
                        conn.Dispose();
                        conn = DatabaseConnection.getDatabaseConnection(connString);
                        conn.Open();
                        cmd = new OracleCommand();
                        cmd.Connection = conn;
                    }
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "Predicates":
                                    part = 1;
                                    break;
                                case "Predicate":
                                    if (part == 1)
                                    {                                        
                                        reader.MoveToNextAttribute();
                                        predicateName = reader.Value;
                                        count++;
                                        query = "insert into Predicate " +
                                                "values('" + predicateName + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteReader();
                                    }
                                    else if (part == 3)
                                    {
                                        reader.MoveToNextAttribute();
                                        predicateName = reader.Value;
                                    }
                                    break;
                                case "Arg":                                    
                                    if (part == 1)
                                    {                                        
                                        reader.MoveToNextAttribute();
                                        id = reader.Value;
                                        
                                        reader.MoveToNextAttribute();
                                        pos = reader.Value;
                                        
                                        reader.MoveToNextAttribute();
                                        attr = reader.Value;
                                        count++;
                                        query = "insert into Argument " +
                                                "values('" + id + "','" + pos + "','" + attr + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteReader();
                                        count++;
                                        query = "insert into HasArgument " +
                                                "values('" + predicateName + "','" + id + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;                                    
                                        cmd.ExecuteReader();
                                    }
                                    else if (part == 3)
                                    {                                        
                                        reader.MoveToNextAttribute();
                                        id = reader.Value;
                                        reader.MoveToNextAttribute();
                                        argName = reader.Value;

                                        count++;
                                        if (preOrCon == 0)
                                        {
                                            query = "insert into Prerequisite " +
                                                    "values('" + hyperAlertTypeName + "','" + predicateName + "','" + id + "','" + argName + "')";
                                        }
                                        else query = "insert into Consequence " +
                                                    "values('" + hyperAlertTypeName + "','" + predicateName + "','" + id + "','" + argName + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteReader();
                                    }

                                    break;
                                case "Implications":
                                    part = 2;
                                    break;
                                case "ImplyingName":
                                    implyPart = 0;
                                    break;
                                case "ImpliedName":
                                    implyPart = 1;
                                    break;
                                case "ImplyingArg":
                                    reader.MoveToNextAttribute();
                                    implyingArg = reader.Value;
                                    break;
                                case "ImpliedArg":                                   
                                    reader.MoveToNextAttribute();
                                    impliedArg = reader.Value;
                                    count++;
                                    query = "insert into Implication " +
                                            "values('" + implyingName + "','" + impliedName + "','" + implyingArg + "','" + impliedArg + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteReader();

                                    break;
                                case "HyperAlertTypes":
                                    part = 3;
                                    break;
                                case "HyperAlertType":                                    
                                    reader.MoveToNextAttribute();
                                    hyperAlertTypeName = reader.Value;                                    
                                    count++;
                                    query = "insert into HyperAlertType " +
                                            "values('" + hyperAlertTypeName + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteReader();
                                    break;
                                case "Fact":                                    
                                    reader.MoveToNextAttribute();
                                    factName = reader.Value;
                                    reader.MoveToNextAttribute();
                                    factType = reader.Value;

                                    count++;
                                    query = "select count(*) from (" +
                                            "select FactName from Fact where FactName = '" + factName + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    rd = cmd.ExecuteReader();
                                        
                                    rd.Read();                                    
                                    if (Convert.ToInt32(rd.GetValue(0)) == 0)
                                    {
                                        count++;
                                        query = "insert into Fact " +
                                                "values('" + factName + "','" + factType + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteReader();
                                    }

                                    count++;
                                    query = "insert into HasFact " +
                                            "values('" + hyperAlertTypeName + "','" + factName + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteReader();
                                    break;
                                case "Protocol":                                    
                                    reader.MoveToNextAttribute();
                                    protocolName = reader.Value;
                                    count++;
                                    query = "select count(*) from (" +
                                            "select ProtocolName from Protocol where ProtocolName = '" + protocolName + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    rd = cmd.ExecuteReader();
                                    
                                    rd.Read();
                                    if (Convert.ToInt32(rd.GetValue(0)) == 0)
                                    {
                                        count++;
                                        query = "insert into Protocol " +
                                                "values('" + protocolName + "')";
                                        cmd.CommandText = query;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteReader();
                                    }

                                    count++;
                                    query = "insert into HasProtocol " +
                                            "values('" + hyperAlertTypeName + "','" + protocolName + "')";
                                    cmd.CommandText = query;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteReader();
                                    break;
                                case "Prerequisite":
                                    preOrCon = 0;
                                    break;
                                case "Consequence":
                                    preOrCon = 1;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            if (implyPart == 0)
                                implyingName = reader.Value;                              
                            else                           
                                impliedName = reader.Value;                                                          
                            break;
                        case XmlNodeType.EndElement:
                            break;
                        default:
                            break;
                    }
                }

                MessageBox.Show("Knowledge Base is created successfully");

            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}