# SignalR
SignalR supports the following techniques for handling real-time communication (in order of graceful fallback):
1) <b>WebSockets</b>
2) <b>Server-Sent Events</b> (i.e. using Event Source)
3) <b>Long Polling</b>

All these protocols is used for handling real-time communication but in general the top is the best (i.e. WebSockets) and the bottom is the worst (i.e. Long-Polling). 

Long-Polling just simulates real-time communication because it does not establish a persistent connection and thats why server cannot push data when available until request is sent from the client.

Whereas WebSockets establishes a persistent connection between the client and server and allows two-way communication. Server-Sent Events also establishes a persistent connection between the client and server but allows one-way communication (i.e. from server to the client).

The way SignalR works is, its going to first try WebSockets, if its doesnt work its going to try Server-Sent Events, if that doesn't work also then finally its going to try Long-Polling. Thats the order. So in this SignalR automatically chooses the best transport method that is within the capabilities of the server and client.
