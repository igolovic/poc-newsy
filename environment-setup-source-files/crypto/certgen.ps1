$testRootCA = New-SelfSignedCertificate -Subject $rootCN -KeyUsageProperty Sign -KeyUsage CertSign -CertStoreLocation Cert:\LocalMachine\My
$identityServerCert = New-SelfSignedCertificate -DnsName $identityServerCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
$webApiCert = New-SelfSignedCertificate -DnsName $webApiCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My

$password = ConvertTo-SecureString -String "password" -Force -AsPlainText

$rootCertPathPfx = "certs"
$identityServerCertPath = "src/IdentityServer/certs"
$webApiCertPath = "src/Api/certs"

Export-PfxCertificate -Cert $testRootCA -FilePath "$rootCertPathPfx/aspnetapp-root-cert.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $identityServerCert -FilePath "$identityServerCertPath/aspnetapp-identity-server.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $webApiCert -FilePath "$webApiCertPath/aspnetapp-web-api.pfx" -Password $password | Out-Null

# Export .cer to be converted to .crt to be trusted within the Docker container.
$rootCertPathCer = "certs/aspnetapp-root-cert.cer"
Export-Certificate -Cert $testRootCA -FilePath $rootCertPathCer -Type CERT | Out-Null

# Trust it on your host machine.
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
$store.Open("ReadWrite")
$store.Add($testRootCA)
$store.Close()