using Microsoft.AspNetCore.Connections;
using ProtoBuf.Grpc.Lite;
using ProtoBuf.Grpc.Lite.Connections;

namespace GrpcLiteWasm.Server;

internal sealed class GrpcTcpHandler : ConnectionHandler
{
    private readonly LiteServer _server;
    public GrpcTcpHandler(LiteServer server) => _server = server;

    public override async Task OnConnectedAsync(ConnectionContext connection) => await _server.ListenAsync(connection.Transport.AsFrames());
}