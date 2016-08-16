using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Utility
{
    public class ResourceFinder
    {
        private string resourceName;
        private Type resourceType;
        private Func<string> accessor;
        private string _resourceValue;

        public ResourceFinder(string resourceName, Type resourceType)
        {
            this.resourceName = resourceName;
            this.resourceType = resourceType;
        }

        public string resourceValue
        {
            get
            {
                SetupAccessor();
                return accessor();
            }
        }

        private void SetupAccessor()
        {
            if (accessor != null) //already set
                return;
            string localValue = _resourceValue;
            bool flag1 = !string.IsNullOrEmpty(resourceName);
            bool flag2 = !string.IsNullOrEmpty(localValue);
            bool flag3 = resourceType != (Type)null;
            if (flag1 == flag2)
            {
                throw new InvalidOperationException("Can't set resource value");
            }
            if (flag3 != flag1)
            {
                throw new InvalidOperationException("Resource name and type required");
            }
            if (flag1)
                PropertyLookup();
            else
            {
                accessor = (Func<string>)(() => localValue);
            }
        }

        private void PropertyLookup()
        {
            if (resourceType == (Type)null || string.IsNullOrEmpty(resourceName))
            {
                throw new InvalidOperationException("Resource name and type required");
            }

            PropertyInfo property = resourceType.GetProperty(resourceName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (property != (PropertyInfo)null)
            {
                MethodInfo getMethod = property.GetGetMethod(true);
                if (getMethod == (MethodInfo)null || !getMethod.IsAssembly && !getMethod.IsPublic)
                    property = (PropertyInfo)null;
            }
            if (property == (PropertyInfo)null)
            {
                throw new InvalidOperationException("Resource type doesn't have property");
            }
            else if (property.PropertyType != typeof(string))
            {
                throw new InvalidOperationException("Resource type must be string");
            }
            else
            {
                accessor = (Func<string>)(() => (string)property.GetValue((object)null, (object[])null));
            }
        }
    }
}