namespace lab6;

class Sportsman
{
    private int num;
    private bool result;

    public delegate void Handler();

    public event Handler? onHit; //влучання
    public event Handler? onMiss; //промах

    public Sportsman(int num)
    {
        this.num = num;
    }

    public bool Result
    {
        get => this.result;
    }

    public void Shoot()
    {
        this.Print();
        Random rnd = new Random();
        if (rnd.Next(0, 2) == 0)
        {
            if (onMiss != null) onMiss();
        }
        else
        {
            if (onHit != null) onHit();
        }
    }

    public void Print()
    {
        Console.WriteLine($"Спортсмен №{this.num}");
    }
}

class Judge
{
    public int count;
    private int hits = 0;

    public delegate void Handler();

    public event Handler? onSuccess; //більше половини влучили
    public event Handler? onFail; //більше половини промзали

    public Judge(int count)
    {
        this.count = count;
        this.onSuccess += this.Success;
        this.onFail += this.Fail;
    }

    public void Hit()
    {
        Console.WriteLine("Влучив!");
        hits++;
    }

    public void Miss() => Console.WriteLine("Промах...");

    public void Results()
    {
        if (hits >= count / 2)
        {
            if (onSuccess != null) onSuccess();
        }
        else
        {
            if (onFail != null) onFail();
        }
    }

    public void Success() => Console.WriteLine("Молодці!");
    public void Fail() => Console.WriteLine("Треба більше тренуватись...");
}

class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        Judge judge = new Judge(rnd.Next(3, 10));
        for (int i = 1; i <= judge.count; i++)
        {
            Sportsman sportsman = new Sportsman(i);
            sportsman.onHit += judge.Hit;
            sportsman.onMiss += judge.Miss;
            sportsman.Shoot();
        }
        Console.Write("\nРезультати змагання: ");
        judge.Results();
    }
}