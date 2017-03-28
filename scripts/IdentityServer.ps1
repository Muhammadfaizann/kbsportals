Import-Module .\New-SelfSignedCertificateEx.psm1 -Force

New-SelfsignedCertificateEx `
	-Subject "CN=identityserver.KBS.Portals" `
	-KeyUsage "KeyEncipherment, DigitalSignature" `
	-StoreLocation "LocalMachine" `
	-KeyLength 4096 `
	-NotAfter $([DateTime]::Now.AddYears(100)) `
	-Exportable