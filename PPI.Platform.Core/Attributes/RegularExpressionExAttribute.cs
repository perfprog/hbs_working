using PPI.Platform.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace PPI.Platform.Core.Attributes
{
    public class RegularExpressionExAttribute : RegularExpressionAttribute, IClientValidatable
    {
        private Regex regex { get; set; }
        private string pattern;

        private string resourceName;
        private Type resourceType;

        /// <summary>
        /// constructor, calls base with ".*" basic regex
        /// </summary>
        /// <param name="resName">resource key</param>
        /// <param name="resType">resource type</param>
        public RegularExpressionExAttribute(string resName, Type resType)
            : base(".*")
        {
            resourceName = resName;
            resourceType = resType;
        }

        /// <summary>
        /// override RegularExpressionAttribute property
        /// </summary>
        public new string Pattern
        {
            get
            {
                SetupRegex();
                return pattern;
            }
        }

        /// <summary>
        /// loads regex from resources
        /// </summary>
        private void SetupRegex()
        {
            ResourceFinder ra = new ResourceFinder(resourceName, resourceType);
            pattern = ra.resourceValue;
            regex = new Regex(pattern);
        }

        /// <summary>
        /// override validation with our regex
        /// </summary>
        /// <param name="value">string for validation</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            SetupRegex();
            string val = Convert.ToString(value);
            if (string.IsNullOrEmpty(val))
                return true;
            var m = regex.Match(val);
            return (m.Success && (m.Index == 0));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metaData, ControllerContext controllerContext)
        {
            yield return new ModelClientValidationRegexRule(base.ErrorMessageString, this.Pattern);
        }
    }
}
