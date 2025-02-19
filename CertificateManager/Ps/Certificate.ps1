# Read PFX Certificate
function Read-CertificateFromPfx {
    param (
        [string]$PfxPath,
        [string]$Password
    )
    $cert = Get-PfxCertificate -FilePath $PfxPath -Password (ConvertTo-SecureString -String $Password -AsPlainText -Force)
    return $cert
}

# Read PEM Certificate (Note: This is simplified, you can use third-party modules for PEM)
function Read-CertificateFromPem {
    param (
        [string]$PemPath
    )
    $cert = Get-PemCertificate -FilePath $PemPath  # You need a PEM parser here
    return $cert
}