using System.Collections.Generic;
using C4Sharp.Models;

namespace C4Sharp.Tests.C4Model.Fixtures
{
    public static class Nodes
    {
        public static DeploymentNode ApacheTomCat(string alias, Container container) =>
            new(alias, "Apache Tomcat", "Apache Tomcat 8.x")
            {
                Properties = new Dictionary<string, string>
                {
                    ["Java Version"] = "8",
                    ["Xmx"] = "512M",
                    ["Xms"] = "1024M",
                },
                Container = container
            };

        public static DeploymentNode OracleNode(string alias, Container container) => 
            new(alias, "Oracle - Primary", "Oracle 12c")
            {
                Container = container
            };

        public static DeploymentNode Ubuntu(string alias, string label, DeploymentNode node) =>
            new(alias, label, "Ubuntu 16.04 LTS")
            {
                Nodes = new[] {node}
            };
        
        public static DeploymentNode Ios(string alias, Container container) =>
            new(alias, "Customer's mobile device", "Apple IOS")
            {
                Container = container
            };        
        
        public static DeploymentNode Android(string alias, Container container) =>
            new(alias, "Customer's mobile device", "Android")
            {
                Container = container
            };

        public static DeploymentNode PersonalComputer(string alias, DeploymentNode node) =>
            new(alias, "Customer's computer", "Mircosoft Windows of Apple macOS")
            {
                Nodes =  new []{node}
            };      
        
        public static DeploymentNode Browser(string alias, Container container) =>
            new(alias, "Web Browser", "Google Chrome, Mozilla Firefox, Apple Safari or Microsoft Edge")
            {
                Container = container
            };         
    }
}