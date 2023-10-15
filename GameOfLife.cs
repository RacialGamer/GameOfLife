namespace GameOfLife;

using System;
using System.Drawing;
using System.Windows.Forms;

public class GameOfLife : Form
{
    private readonly int _width = 1900;
    private readonly int _height = 1000;
    private int _cellSize = 10;
    private int _gridWidth;
    private int _gridHeight;
    private readonly Color _bgColor = Color.Black;
    private Color _cellColor = Color.FromArgb(54, 69, 79);
    
    private bool[,]? _grid;
    private bool _paused;
    private readonly Timer _timer = new ();
    private readonly Button _clearButton = new ();
    private readonly Button _pauseButton = new ();
    private readonly Button _randomizeButton = new ();
    private readonly TrackBar _speedSlider = new ();
    private readonly TrackBar _gridSizeSlider = new ();
    private readonly TextBox _hexTextBox = new ();
    private bool _drawing;
    private bool _erasing;
    
    public GameOfLife()
    {
        Text = "Game of Life";
        Size = new Size(_width, _height);
        DoubleBuffered = true;

        InitializeUi();

        _cellSize = _gridSizeSlider.Value;
        _gridWidth = _width / _cellSize;
        _gridHeight = _height / _cellSize;
        InitializeGrid();

        // Set up timer for updating the grid
        _timer.Interval = 1000; // Default interval, can be adjusted
        _timer.Tick += (sender, e) =>
        {
            if (!_paused)
                UpdateGrid();
            this.Invalidate();
        };
        _timer.Start();
        
        this.MouseClick += (sender, e) =>
        {
            int x = e.X / _cellSize;
            int y = e.Y / _cellSize;

            if (e.Button == MouseButtons.Left)
            {
                _grid[x, y] = true; // Set cell state to alive
            }
            else if (e.Button == MouseButtons.Right)
            {
                _grid[x, y] = false; // Set cell state to dead
            }

            this.Invalidate();
        };

        this.MouseDown += (sender, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                _drawing = true;
                _erasing = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _erasing = true;
                _drawing = false;
            }
        };

        this.MouseUp += (sender, e) =>
        {
            if (e.Button == MouseButtons.Left)
                _drawing = false;
            else if (e.Button == MouseButtons.Right)
                _erasing = false;
        };

        this.MouseMove += (sender, e) =>
        {
            int x = e.X / _cellSize;
            int y = e.Y / _cellSize;

            if (_drawing)
            {
                _grid[x, y] = true; // Set cell state to alive
                this.Invalidate();
            }
            else if (_erasing)
            {
                _grid[x, y] = false; // Set cell state to dead
                this.Invalidate();
            }
        };
    }

    private void InitializeUi()
    {
        _clearButton.Text = "Clear Grid";
        _clearButton.Location = new Point(10, 10);
        _clearButton.Click += (sender, e) =>
        {
            Clear();
        };
        Controls.Add(_clearButton);
        
        _pauseButton.Text = "Pause";
        _pauseButton.Location = new Point(10, 40);
        _pauseButton.Click += (sender, e) =>
        {
            _paused = !_paused;
            _pauseButton.Text = _paused ? "Play" : "Pause";
        };
        Controls.Add(_pauseButton);

        _randomizeButton.Text = "Randomize";
        _randomizeButton.Location = new Point(10, 70);
        _randomizeButton.Click += (sender, e) =>
        {
            InitializeGrid();
            Invalidate();
        };
        Controls.Add(_randomizeButton);

        _speedSlider.Minimum = 1;
        _speedSlider.Maximum = 1000; // Adjust the maximum value as needed
        _speedSlider.Value = _timer.Interval;
        _speedSlider.Location = new Point(10, 100);
        _speedSlider.Scroll += (sender, e) =>
        {
            _timer.Interval = _speedSlider.Value;
        };
        Controls.Add(_speedSlider);

        _gridSizeSlider.Minimum = 1;
        _gridSizeSlider.Maximum = 50; // Adjust the maximum value as needed
        _gridSizeSlider.Value = _cellSize;
        _gridSizeSlider.Location = new Point(10, 140);
        _gridSizeSlider.Scroll += (sender, e) =>
        {
            _cellSize = _gridSizeSlider.Value;
            _gridWidth = _width / _cellSize;
            _gridHeight = _height / _cellSize;
            
            InitializeGrid();
            Clear();
            Invalidate();
        };
        Controls.Add(_gridSizeSlider);
        
        _hexTextBox.Text = "36454F";
        _hexTextBox.Location = new Point(10, 190);
         _hexTextBox.TextChanged += (sender, e) =>
         { 
             try
            {
               _cellColor = Color.FromArgb(Convert.ToInt32("FF" + _hexTextBox.Text, 16));
               Invalidate();
            } catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
         };
        Controls.Add(_hexTextBox);
        
                    

    }
    
    private void InitializeGrid()
    {
        _grid = new bool[_width / _cellSize, _height / _cellSize];
        Random rand = new Random();
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                _grid[x, y] = rand.Next(2) == 1;
            }
        }
    }

    void Clear()
    {
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                _grid[x, y] = false;
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;

        g.Clear(_bgColor);
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                if (_grid[x, y])
                {
                    g.FillRectangle(
                        new SolidBrush(_cellColor),
                        x * _cellSize, y * _cellSize, _cellSize, _cellSize
                    );
                }
            }
        }
    }

    private void UpdateGrid()
    {
        bool[,] newGrid = new bool[_gridWidth, _gridHeight];
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                int neighbors = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        int nx = x + i;
                        int ny = y + j;
                        if (nx >= 0 && ny >= 0 && nx < _gridWidth && ny < _gridHeight)
                        {
                            if (_grid[nx, ny])
                            {
                                neighbors++;
                            }
                        }
                    }
                }

                if (_grid[x, y])
                {
                    if (neighbors < 2 || neighbors > 3)
                    {
                        newGrid[x, y] = false;
                    }
                    else
                    {
                        newGrid[x, y] = true;
                    }
                }
                else
                {
                    if (neighbors == 3)
                    {
                        newGrid[x, y] = true;
                    }
                }
            }
        }
        _grid = newGrid;
    }
}
