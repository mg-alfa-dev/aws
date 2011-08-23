using System.Management.Automation;

// ReSharper disable InconsistentNaming

namespace AwsPs
{
    [Cmdlet(VerbsCommon.Get, "Instance")]
    public class Get_Instance : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientUtilsProvider.GetClient();
            WriteObject(client.GetInstances());
        }
    }

    [Cmdlet("Start", "Instance")]
    public class Start_Instance : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InstanceId;
        
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientUtilsProvider.GetClient();
            client.StartInstance(InstanceId);
            WriteObject("Started successfully");
        }
    }

    [Cmdlet("Stop", "Instance")]
    public class Stop_Instance : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InstanceId;
        
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientUtilsProvider.GetClient();
            client.StopInstance(InstanceId);
            WriteObject("Stopped successfully");
        }
    }
}