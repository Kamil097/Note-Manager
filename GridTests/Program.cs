using Snake;


//Visualizer.SnakeAnimation();
Board board = new Board(9, 5);
MySnake snake = new MySnake(MySnake.Speed.Slow);
var frame = Visualizer.GenerateFrame(snake, board);
Visualizer.GenerateBoard(frame);    