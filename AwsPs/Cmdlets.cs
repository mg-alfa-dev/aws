using System.Management.Automation;

// ReSharper disable InconsistentNaming

namespace AwsPs
{
    [Cmdlet(VerbsCommon.Get, "AwsInstance")]
    public class Get_Instance : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientProvider.GetClient();
            WriteObject(client.GetInstances());
        }
    }

    [Cmdlet("Start", "AwsInstance")]
    public class Start_Instance : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InstanceId;
        
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientProvider.GetClient();
            client.StartInstance(InstanceId);
            WriteObject("Started successfully");
        }
    }

    [Cmdlet("Stop", "AwsInstance")]
    public class Stop_Instance : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)] 
        public string InstanceId;
        
        protected override void ProcessRecord()
        {
            WriteObject("Sending AWS request...");
            var client = ClientProvider.GetClient();
            client.StopInstance(InstanceId);
            WriteObject("Stopped successfully");
        }
    }
}