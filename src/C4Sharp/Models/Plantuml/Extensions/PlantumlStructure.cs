using System.Linq;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Plantuml.Extensions
{
    /// <summary>
    /// PlantUML Parser
    /// </summary>
    internal static class PlantumlStructure
    {
        /// <summary>
        /// Parser Structure to PlantUML
        /// </summary>        
        public static string ToPumlString(this Structure structure)
        {
            return structure switch
            {
                Person person => person.ToPumlString(),
                SoftwareSystem system => system.ToPumlString(),
                SoftwareSystemBoundary softwareSystemBoundary => softwareSystemBoundary.ToPumlString(), 
                DeploymentNode deploymentNode => deploymentNode.ToPumlString(),
                Component component => component.ToPumlString(),
                Container container => container.ToPumlString(),
                ContainerBoundary containerBoundary => containerBoundary.ToPumlString(),
                EnterpriseBoundary enterpriseBoundary => enterpriseBoundary.ToPumlString(),
                _ => string.Empty
            };
        }
        
        private static string ToPumlString(this Person person)
        {
            var procedureName = $"Person{GetExternalSuffix(person)}";

            return $"{procedureName}({person.Alias}, \"{person.Label}\", \"{person.Description}\", $link=\"{person.Link}\")";
        }        
        
        private static string ToPumlString(this SoftwareSystem system)
        {
            var procedureName = $"System{GetExternalSuffix(system)}";

            return $"{procedureName}({system.Alias}, \"{system.Label}\", \"{system.Description}\", $link=\"{system.Link}\")";
        }        
        
        private static string ToPumlString(this SoftwareSystemBoundary boundary)
        {
            var stream = new StringBuilder();
            stream.AppendLine();
            stream.AppendLine($"System_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");

            foreach (var container in boundary.Containers)
            {
                stream.AppendLine($"{SpaceMethods.Indent()}{container.ToPumlString()}");
            }

            stream.AppendLine("}");

            return stream.ToString();
        } 
        
        private static string ToPumlString(this EnterpriseBoundary boundary)
        {
            var stream = new StringBuilder();
            stream.AppendLine();
            stream.AppendLine($"Enterprise_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");

            foreach (var structure in boundary.Structures)
            {
                if (structure is (Person or SoftwareSystem or EnterpriseBoundary))
                {
                    stream.AppendLine($"{SpaceMethods.Indent()}{structure.ToPumlString()}");
                }
            }

            stream.AppendLine("}");

            return stream.ToString();
        }         
        
        private static string ToPumlString(this Component component)
        {
            var procedureName = $"Component{GetExternalSuffix(component)}";

            return $"{procedureName}({component.Alias}, \"{component.Label}\", \"{component.Technology}\", \"{component.Description}\", $link=\"{component.Link}\")";
        }     
        
        private static string ToPumlString(this Container container)
        {
            var externalSuffix = GetExternalSuffix(container);

            var procedureName = container.ContainerType switch
            {
                ContainerType.Database => $"ContainerDb{externalSuffix}",
                ContainerType.Queue => $"ContainerQueue{externalSuffix}",
                _ => $"Container{externalSuffix}"
            };

            return  $"{procedureName}({container.Alias}, \"{container.Label}\", \"{container.Technology}\", \"{container.Description}\", $link=\"{container.Link}\")";
        }

        private static string ToPumlString(this ContainerBoundary boundary)
        {
            var stream = new StringBuilder();

            stream.AppendLine();
            stream.AppendLine($"Container_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");
            foreach (var component in boundary.Components)
            {
                stream.AppendLine($"{SpaceMethods.Indent()}{component.ToPumlString()}");
            }

            if (boundary.Relationships.Any())
            {
                stream.AppendLine();
                foreach (var relationship in boundary.Relationships)
                {
                    stream.AppendLine($"{SpaceMethods.Indent()}{relationship.ToPumlString()}");
                }
            }

            stream.AppendLine("}");

            return stream.ToString();
        }
        
        private static string ToPumlString(this DeploymentNode deployment, int concat = 0)
        {
            var stream = new StringBuilder();
            var spaces = SpaceMethods.Indent(concat);

            if (concat == 0)
            {
                stream.AppendLine();
            }

            if (deployment.Properties.Any())
            {
                foreach (var (key, value) in deployment.Properties)
                {
                    stream.AppendLine($"{spaces}AddProperty(\"{key}\", \"{value}\")");
                }
            }

            stream.AppendLine(!deployment.Tags.Any()
                ? $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\") {{"
                : $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\", $tags=\"{string.Join(',', deployment.Tags)}\") {{");

            if (deployment.Nodes.Any())
            {
                foreach (var node in deployment.Nodes)
                {
                    stream.AppendLine($"{node.ToPumlString(concat + SpaceMethods.TabSize)}");
                }
            }

            if (deployment.Container != null)
            {
                stream.AppendLine(SpaceMethods.Indent(concat) + deployment.Container.ToPumlString());
            }

            stream.Append(spaces + "}");

            return stream.ToString();
        }

        private static string GetExternalSuffix(Structure structure) => structure.Boundary == Boundary.External ? "_Ext" : string.Empty;
    }
}