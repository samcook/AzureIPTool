# Azure IP Tool

A small tool to help extract information from the Azure IP ranges and Service Tags data available from [here](https://www.microsoft.com/en-us/download/details.aspx?id=56519).

## Usage

```
AzureIPTool.exe <path to service tags json file> <function> [<function options>]

Functions:
- servicetagids
- openvpnroutes <service tag id 1> ... <service tag id n>
- servicetagsforips <ip address 1> ... <ip address n>

e.g.
- AzureIPTool.exe ServiceTags_Public_20210111.json openvpnroutes Sql.WestUS2 Storage.WestUS2
- AzureIPTool.exe ServiceTags_Public_20210111.json servicetagsforips 13.66.226.202 13.66.228.204
```

## Example output

### Service Tags for IPs

Print out service tags for specified IPs

```
AzureIPTool.exe "Data\ServiceTags_Public_20210215.json" servicetagsforips 13.66.226.202 13.66.228.204
```
```
13.66.226.202
- Sql
- Sql.WestUS2
- AzureCloud.westus2
- AzureCloud
13.66.228.204
- EventHub
- EventHub.WestUS2
- AzureCloud.westus2
- AzureCloud
```

### OpenVPN Routes

Generates push routes for OpenVPN configuration

```
AzureIPTool.exe "Data\ServiceTags_Public_20210215.json" openvpnroutes Sql.WestUS2 Sql.CentralUS
```
```
; Sql.WestUS2 (ServiceTags_Public_20210215.json)
push "route 13.66.136.0 255.255.255.192"
push "route 13.66.136.192 255.255.255.248"
push "route 13.66.137.0 255.255.255.192"
push "route 13.66.226.202 255.255.255.255"
push "route 13.66.229.222 255.255.255.255"
push "route 13.66.230.18 255.255.255.254"
push "route 13.66.230.60 255.255.255.255"
push "route 13.66.230.64 255.255.255.255"
push "route 13.66.230.103 255.255.255.255"
push "route 20.51.9.128 255.255.255.128"
push "route 40.78.240.0 255.255.255.192"
push "route 40.78.240.192 255.255.255.248"
push "route 40.78.241.0 255.255.255.192"
push "route 40.78.248.0 255.255.255.192"
push "route 40.78.248.192 255.255.255.248"
push "route 40.78.249.0 255.255.255.192"
push "route 52.191.144.64 255.255.255.192"
push "route 52.191.152.64 255.255.255.192"
push "route 52.191.172.187 255.255.255.255"
push "route 52.191.174.114 255.255.255.255"
push "route 52.229.17.93 255.255.255.255"
push "route 52.246.251.248 255.255.255.255"

; Sql.CentralUS (ServiceTags_Public_20210215.json)
push "route 13.67.215.62 255.255.255.255"
push "route 13.89.36.110 255.255.255.255"
push "route 13.89.37.61 255.255.255.255"
push "route 13.89.57.50 255.255.255.255"
push "route 13.89.57.115 255.255.255.255"
push "route 13.89.168.0 255.255.255.192"
push "route 13.89.168.192 255.255.255.248"
push "route 13.89.169.0 255.255.255.192"
push "route 20.40.228.128 255.255.255.128"
push "route 23.99.160.139 255.255.255.255"
push "route 23.99.160.140 255.255.255.254"
push "route 23.99.160.142 255.255.255.255"
push "route 23.99.205.183 255.255.255.255"
push "route 40.69.132.90 255.255.255.255"
push "route 40.69.143.202 255.255.255.255"
push "route 40.69.169.120 255.255.255.255"
push "route 40.69.189.48 255.255.255.255"
push "route 40.77.30.201 255.255.255.255"
push "route 40.86.75.134 255.255.255.255"
push "route 40.113.200.119 255.255.255.255"
push "route 40.122.205.105 255.255.255.255"
push "route 40.122.215.111 255.255.255.255"
push "route 52.165.184.67 255.255.255.255"
push "route 52.173.205.59 255.255.255.255"
push "route 52.176.43.167 255.255.255.255"
push "route 52.176.59.12 255.255.255.255"
push "route 52.176.95.237 255.255.255.255"
push "route 52.176.100.98 255.255.255.255"
push "route 52.182.136.0 255.255.255.192"
push "route 52.182.136.192 255.255.255.248"
push "route 52.182.137.0 255.255.255.192"
push "route 104.43.164.21 255.255.255.255"
push "route 104.43.164.247 255.255.255.255"
push "route 104.43.203.72 255.255.255.255"
push "route 104.208.21.0 255.255.255.192"
push "route 104.208.21.192 255.255.255.248"
push "route 104.208.22.0 255.255.255.192"
push "route 104.208.28.16 255.255.255.255"
push "route 104.208.28.53 255.255.255.255"
```