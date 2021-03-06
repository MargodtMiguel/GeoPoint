import { HttpClient } from "./HttpClient";
import { ILogger } from "./ILogger";
import { ITransport, TransferFormat } from "./ITransport";
/** @private */
export declare class LongPollingTransport implements ITransport {
    private readonly httpClient;
    private readonly accessTokenFactory;
    private readonly logger;
    private readonly logMessageContent;
    private url;
    private pollAbort;
    private shutdownTimer;
    private shutdownTimeout;
    private running;
    private stopped;
    readonly pollAborted: boolean;
    constructor(httpClient: HttpClient, accessTokenFactory: () => string | Promise<string>, logger: ILogger, logMessageContent: boolean, shutdownTimeout?: number);
    connect(url: string, transferFormat: TransferFormat): Promise<void>;
    private updateHeaderToken;
    private poll;
    send(data: any): Promise<void>;
    stop(): Promise<void>;
    onreceive: (data: string | ArrayBuffer) => void;
    onclose: (error?: Error) => void;
}
