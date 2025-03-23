# In local PC's certificate-store create root certificate for development-purpose Certificate Authority, which will sign other certificates.
$testRootCA = New-SelfSignedCertificate `
   -DnsName "root_ca_dev_test.com", "root_ca_dev_test.com" `
   -CertStoreLocation "Cert:\LocalMachine\My" `
   -NotAfter (Get-Date).AddYears(20) `
   -FriendlyName "root_ca_dev_test.com" `
   -KeyUsageProperty All `
   -KeyUsage CertSign, CRLSign, DigitalSignature 

# In local PC's certificate-store create development-purpose certificate for API application.
$webApiCert = New-SelfSignedCertificate -DnsName "dev_test_webapi.com" -Signer $testRootCA -CertStoreLocation "Cert:\LocalMachine\My"

# In local PC's certificate-store create development-purpose certificate for IdentityServer application.
$identityServerCert = New-SelfSignedCertificate -DnsName "dev_test_identityserver.com" -Signer $testRootCA -CertStoreLocation "Cert:\LocalMachine\My"

$password = ConvertTo-SecureString -String "password" -Force -AsPlainText

# Follow folder structure in these paths to regenerate files in right locations
$rootCertPathPfx = "certs"
$identityServerCertPath = "src/IdentityServer/certs"
$webApiCertPath = "src/Api/certs"

# Export root certificate from local PC's certificate-store to .pfx (certificates with private keys), and copy to locations where they will be taken by Docker to be copied into the container/image
Export-PfxCertificate -Cert $testRootCA -FilePath "$rootCertPathPfx/root.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $identityServerCert -FilePath "$identityServerCertPath/identityserver.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $webApiCert -FilePath "$webApiCertPath/newsy_web.pfx" -Password $password | Out-Null

# Export root certificate from local PC's certificate-store to .cer, and copy to location where it will be taken by Docker to be copied into the container/image
$rootCertPathCer = "certs/root.cer"
Export-Certificate -Cert $testRootCA -FilePath $rootCertPathCer -Type CERT | Out-Null

# Trust it on your host machine.
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
$store.Open("ReadWrite")
$store.Add($testRootCA)
$store.Close()

echo "Script finished - check if certificates were created in 'certs' and 'src' folders located in root folder"