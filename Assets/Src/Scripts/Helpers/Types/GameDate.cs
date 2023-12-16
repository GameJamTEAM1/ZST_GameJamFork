public class GameDate {
    private byte week;
    private byte month;
    private uint year;

    public byte Week {
        private set { }
        get { return this.week; }
    }

    public byte Month {
        private set { }
        get { return this.month; }
    }

    public uint Year {
        private set { }
        get { return this.year; }
    }

    public GameDate() {
        this.year = 2024;
        this.month = 1;
        this.week = 1;
    }

    public GameDate(uint year, byte month, byte week) {
        this.year = year;
        this.month = month;
        this.week = week;
    }

    public void AddWeek() {
        if (this.week + 1 > 4) {
            this.week = 1;
            AddMonth();
        }
        else {
            this.week++;
        }
    }

    public void AddMonth() {
        if (this.month + 1 > 12) {
            this.month = 1;
            AddYear();
        }
        else {
            this.month++;
        }
    }

    public void AddYear() {
        this.year++;
    }

    public static bool operator== (GameDate a, GameDate b) {
        if ((object)a == (object)b) return true;

        return (a.Year == b.Year && a.Month == b.Month && a.Week == b.Week);
    }

    public static bool operator!=(GameDate a, GameDate b) {
        return !(a.Year == b.Year && a.Month == b.Month && a.Week == b.Week);
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) return false;

        var date = (GameDate)obj;

        return (this.Year == date.Year && this.Month == date.Month && this.Week == date.Week);
    }

    public override int GetHashCode() {
        return this.Year.GetHashCode() ^ this.Month.GetHashCode() ^ this.Week.GetHashCode();
    }
}
