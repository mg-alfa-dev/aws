REM Path should have bin\java.exe under it 
set JAVA_HOME="C:\Program Files (x86)\java\jre6"
REM Path to Primary Key and Certificate retrieved from AWS 
set EC2_PRIVATE_KEY=c:\projects\aws\aws-pk.pem 
set EC2_CERT=c:\projects\aws\aws-cer.pem
REM Path to EC2 API, subfolders of bin and lib 
set EC2_HOME=c:\projects\aws\ec2
set PATH=%PATH%;%EC2_HOME%\bin
REM Path to ELB API, subfolders of bin and lib 
REM set AWS_ELB_HOME=c:\projects\aws\elb 
REM set PATH=%PATH%;%AWS_ELB_HOME%\bin
cls
cmd