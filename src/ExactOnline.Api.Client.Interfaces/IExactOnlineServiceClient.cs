// See https://github.com/StefH/ProxyInterfaceSourceGenerator

using ProxyInterfaceGenerator;

namespace ExactOnline.Api.Client;

[Proxy<ExactOnlineServiceClient>(true)]
public partial interface IExactOnlineServiceClient;