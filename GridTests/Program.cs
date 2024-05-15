using Snake;




Board board = new Board(20, 10);
MySnake snake = new MySnake(MySnake.Speed.Fast);
Game game = new Game(snake, board);
game.Run();
/*
var frame = Visualizer.GenerateFrame(snake, board);
Visualizer.GenerateBoard(frame);    */