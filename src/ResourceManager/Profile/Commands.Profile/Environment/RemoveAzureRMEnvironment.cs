﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.Profile.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.WindowsAzure.Commands.Common;

namespace Microsoft.Azure.Commands.Profile
{
    /// <summary>
    /// Cmdlet to remove Azure Environment from Profile.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmEnvironment")]
    [OutputType(typeof(PSAzureEnvironment))]
    public class RemoveAzureRMEnvironmentCommand : AzureRMCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true,
            HelpMessage = "The environment name")]
        public string Name { get; set; }

        [Parameter(Position = 1, HelpMessage = "Do not confirm deletion of subscription")]
        public SwitchParameter Force { get; set; }

        protected override void BeginProcessing()
        {
            // do not call begin processing there is no context needed for this cmdlet
        }


        protected override void ProcessRecord()
        {
            var profileClient = new RMProfileClient(AzureRmProfileProvider.Instance.Profile);

            ConfirmAction(
                Force.IsPresent,
                string.Format(
                    "Removing an environment will remove all associated subscriptions and accounts. Are you sure you want to remove an environment '{0}'?", 
                    Name),
                "Removing environment",
                Name,
                () => WriteObject((PSAzureEnvironment) profileClient.RemoveEnvironment(Name)));
        }
    }
}
