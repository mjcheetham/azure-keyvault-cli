# Azure Key Vault Command Line Tool

## Overview

This command line tool allows simple access to list and read secrets from Azure Key Vaults.

## Usage and examples

### Managing user vaults

#### Register a known vault for the current user

```
kv vault add <name> <url>
```

##### Examples

```
$ kv vault add foo "https://foo.vault.azure.net"
$ kv vault add bar "https://bar.vault.azure.net"
$ kv vault add fish "https://fish.vault.azure.net"
```

#### Remove a known vault for the current user

```
kv vault remove <name>
```

##### Examples

```
$ kv vault remove fish
```

#### List all known vaults for the current user

```
kv vault list [--verbose]
```

##### Examples

```
$ kv vault list
foo: https://foo.vault.azure.net
bar: https://bar.vault.azure.net
```

#### Rename a known vault for the current user

```
kv vault rename <old-name> <new-name>
```

##### Examples

```
$ kv vault rename bar altbar
```

### Managing authentication configuration

#### Add/set authentication configuration for a vault URL or URL regular expression

```
kv auth add --url <url> --clientid <client-id> [--thumbprint <cert-thumbprint>]
```

##### Examples

```
$ kv auth add --url "https://example.vault.azure.net" --clientid "40E419BC-DF98-4456-9B75-9A2C9C67AE02"
$ kv auth add --url "https://fish.vault.azure.net" --clientid "40E419BC-DF98-4456-9B75-9A2C9C67AE02" --thumbprint "25DBE...36EA8"
$ kv auth add --url "https:\/\/(foo|bar)\d+.vault.azure.net" --clientid "40E419BC-DF98-4456-9B75-9A2C9C67AE02"
```

#### Remove an authentication configuration for a vault URL or URL regular expression

```
kv auth remove --url <url>
```

##### Examples

```
$ kv auth remove --url "https://example.vault.azure.net"
```

#### List all authentication configurations

```
kv auth list
```

##### Examples

```
$ kv auth list
{
  "http://fish.vault.azure.net": {
    "client": "40E419BC-DF98-4456-9B75-9A2C9C67AE02",
    "certThumbprint": "25DBE...36EA8"
  },
  "https:\\/\\/(foo|bar)\\d+.vault.azure.net": {
    "client": "40E419BC-DF98-4456-9B75-9A2C9C67AE02",
    "certThumbprint": null
  }
}
```

### Managing secrets

#### List all secrets in a vault

```
kv list <vault> [--verbose]
```

##### Examples

```
$ kv list foo
mysecret42
supersecret
third-secret
```

#### Retrieve a secret as plain-text

```
kv get <vault> <secret> [--force] [--verbose]
```

##### Examples

```
$ kv get foo supersecret
INFO: Secret value is masked; '--force' option is not present
********

$ kv get foo supersecret --force
thisisthepassword
```

#### Create/update a secret

```
kv set <vault> <secret> <value>
```

##### Examples

```
$ kv set foo supersecret b3tt3rPa55w0rd
INFO: Secret 'supersecret' was set successfully
```

#### Delete a secret

```
kv delete <vault> <secret> --force [--verbose]
```

##### Examples

```
$ kv delete foo supersecret --force
INFO: Secret 'supersecret' was deleted successfully
```

## Limitations

- This tool does not support Key Vault _certificates_ or _keys_ at this time.
- To be able to authenticate with a Key Vault, you must have first configured an AAD Application (and know it's ID _(client ID)_) with 'Get' and 'List' permissions for secrets. In a future version the goal is to add the capability to automatically provision this.

## Disclaimer

This tool is **not** an official Microsoft product and is **not** endorsed as such. "_Azure_", "_Key Vault_" and related product names remain Microsoft trademarks or otherwise terms owned by Microsoft.
