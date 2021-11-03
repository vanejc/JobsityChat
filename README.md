# JobsityChat
All mandatory functions were done
● Allow registered users to log in and talk with other users in a chatroom.
● Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code
● Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code)
● The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.
● Have the chat messages ordered by their timestamps and show only the last 50
messages.
● Unit test the functionality you prefer.
Bonus that were done
● Use .NET identity for users authentication
● Handle messages that are not understood or any exceptions raised within the bot.
