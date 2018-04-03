using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WorkOutDBModel.Model;

namespace WorkOutDBLayer
{
    public class WorkOutActiveDb
    {
        WorkOutDBLayer.MyContext db = null;
        public WorkOutActiveDb()
        {
            db = new WorkOutDBLayer.MyContext();
        }

        public List<Workout_Active> GetAll()
        {
            try
            {
                return db.WorkOutsActive.ToList();
            }
            catch (Exception ex)
            {
                return new List<Workout_Active>();
            }
        }


        public Workout_Active GetById(int Id)
        {
            try
            {
                return db.WorkOutsActive.Find(Id);
            }
            catch (Exception ex)
            {
                return new Workout_Active();
            }
        }

        public workoutactive StartWorkOut(int Id)
        {
            try
            {

                WorkOut workout = db.WorkOutCollection.FirstOrDefault(a => a.WorkOutId == Id);

                workoutactive result = new workoutactive()
                {
                    WorkOutId = workout.WorkOutId,
                    WorkOutComment = "",
                    WorkOutTitle = workout.WorkOutTitle,
                    StartDate = DateTime.Now,
                    StartTime = DateTime.Now.ToLocalTime().ToShortTimeString()
                };
                return result;

            }
            catch (Exception ex)
            {
                return new workoutactive();
            }
        }
        public bool AddStartWorkOut(Workout_Active model)
        {
            try
            {
                if (model.Id > 0)
                {
                    Workout_Active editworkoutactive = db.WorkOutsActive.Where(a => a.Id == model.Id && a.WorkOutId == model.WorkOutId).FirstOrDefault();
                    if (editworkoutactive != null)
                    {
                        model.Start_Date = editworkoutactive.Start_Date;
                        model.Start_Time = editworkoutactive.Start_Time;
                        db.Entry(editworkoutactive).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.WorkOutsActive.Add(model);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public workoutactive EndWorkOut(int Id, int WorkOutId)
        {
            try
            {
                workoutactive result = null;
                WorkOut workout = db.WorkOutCollection.FirstOrDefault(a => a.WorkOutId == WorkOutId);

                Workout_Active workoutactive = db.WorkOutsActive.FirstOrDefault(a => a.WorkOutId == WorkOutId && a.Id == Id);

                if (workout != null && workoutactive != null)
                {
                    result = new workoutactive()
                    {
                        Id = workoutactive.Id,
                        WorkOutId = workout.WorkOutId,
                        WorkOutComment = workoutactive.Comment,
                        WorkOutTitle = workout.WorkOutTitle,
                        StartDate = DateTime.Now,
                        StartTime = DateTime.Now.ToLocalTime().ToShortTimeString()
                    };
                }

                return result;

            }
            catch (Exception ex)
            {
                return new workoutactive();
            }
        }


        public WorkOutSummary GetWorkOutSummary()
        {
            int dayWorkOut = 0, weekWorkOut = 0, monthWorkOut = 0;
            try
            {



                List<Workout_Active> results = db.WorkOutsActive.Where(a => a.End_Date != null && a.End_Date.Value.Day == DateTime.Now.Day).ToList();
                foreach (Workout_Active workoutactive in results)
                {
                    TimeSpan span = Convert.ToDateTime(workoutactive.End_time).Subtract(Convert.ToDateTime(workoutactive.Start_Time));
                    dayWorkOut += Convert.ToInt32(span.TotalMinutes);

                }
                results = db.WorkOutsActive.Where(a => a.End_Date != null).ToList();
                foreach (Workout_Active workoutactive in results)
                {
                    if (GetWeekNumber(workoutactive.End_Date) == GetWeekNumber(DateTime.Now))
                    {
                        TimeSpan span = Convert.ToDateTime(workoutactive.End_time).Subtract(Convert.ToDateTime(workoutactive.Start_Time));
                        weekWorkOut += Convert.ToInt32(span.TotalMinutes);
                    }
                }

                results = db.WorkOutsActive.Where(a => a.End_Date != null && a.End_Date.Value.Month == DateTime.Now.Month).ToList();
                foreach (Workout_Active workoutactive in results)
                {
                    TimeSpan span = Convert.ToDateTime(workoutactive.End_time).Subtract(Convert.ToDateTime(workoutactive.Start_Time));
                    monthWorkOut += Convert.ToInt32(span.TotalMinutes);
                }
                WorkOutSummary result = new WorkOutDBLayer.WorkOutActiveDb.WorkOutSummary()
                {
                    CurrentDay = dayWorkOut,
                    CurrentMonth = monthWorkOut,
                    CurrentWeek = weekWorkOut
                };
                return result;
            }

            catch (Exception ex)
            {
                return new WorkOutSummary();
            }

        }


        public ChartSummary GetWeeklyChart()
        {

            try
            {


                List<Workout_Active> tempresults = new List<Workout_Active>();
                ChartSummary chart = new WorkOutDBLayer.WorkOutActiveDb.ChartSummary();
                foreach (Workout_Active workoutactive in db.WorkOutsActive.Where(a => a.End_Date != null).ToList())
                {
                    if (GetWeekNumber(workoutactive.End_Date) == GetWeekNumber(DateTime.Now))
                        tempresults.Add(workoutactive);
                }

                chart.ChartData = new List<int>();
                chart.ChartLabel = new List<string>() { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Monday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Tuesday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Wednesday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Thursday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Friday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Saturday));
                chart.ChartData.Add(GetDayChart(tempresults, DayOfWeek.Sunday));
                return chart;
            }

            catch (Exception ex)
            {
                return new ChartSummary();
            }

        }

        public ChartSummary GetMonthlyChart()
        {

            List<Workout_Active> tempresults = new List<Workout_Active>();
            ChartSummary chart = new WorkOutDBLayer.WorkOutActiveDb.ChartSummary();
            try
            {
                chart.ChartData = new List<int>();
                int Calorie = 0;
                DateTime reference = DateTime.Now;
                Calendar calendar = CultureInfo.CurrentCulture.Calendar;

                IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

                List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                    .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                    .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                    .ToList();
                weeks.ForEach(x => {
                    foreach (Workout_Active item in db.WorkOutsActive.Where(a => a.End_Date >= x.Item1 && a.End_Date <= x.Item2).ToList())
                    {
                        Calorie += db.WorkOutCollection.FirstOrDefault(a => a.WorkOutId == item.WorkOutId).CaloriesBurnPerMin;
                    }
                    chart.ChartData.Add(Calorie);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            return chart;
        }


        public ChartSummary GetYearlyChart()
        {
            List<Workout_Active> tempresults = new List<Workout_Active>();
            ChartSummary chart = new WorkOutDBLayer.WorkOutActiveDb.ChartSummary();
            chart.ChartData = new List<int>();
            chart.ChartLabel = new List<string>();
            try
            {


                int Calorie = 0;
                for (int i = 1; i < 13; i++)
                {
                    Calorie = 0;
                    foreach (Workout_Active workoutactive in db.WorkOutsActive.Where(a => a.End_Date != null && a.End_Date.Value.Year == DateTime.Now.Year && a.End_Date.Value.Month == i).ToList())
                    {
                        Calorie += db.WorkOutCollection.FirstOrDefault(a => a.WorkOutId == workoutactive.WorkOutId).CaloriesBurnPerMin;
                    }
                    chart.ChartLabel.Add(i.ToString());
                    chart.ChartData.Add(Calorie);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return chart;
        }


        private int GetDayChart(List<Workout_Active> tempresults, DayOfWeek dayofWeek)
        {
            int Calorie = 0;
            try
            {
                if (tempresults.Where(a => a.End_Date.Value.DayOfWeek == dayofWeek).Any())
                {
                    foreach (Workout_Active item in tempresults.Where(a => a.End_Date.Value.DayOfWeek == dayofWeek).ToList())
                    {
                        Calorie += db.WorkOutCollection.FirstOrDefault(a => a.WorkOutId == item.WorkOutId).CaloriesBurnPerMin;
                    }
                }
            }
            catch (Exception ex)
            {
                Calorie = 0;
            }
            return Calorie;
        }


        public class ChartSummary
        {
            public List<string> ChartLabel { get; set; }
            public List<int> ChartData { get; set; }
        }
        public class WorkOutSummary
        {
            public int CurrentDay { get; set; }
            public int CurrentWeek { get; set; }
            public int CurrentMonth { get; set; }
        }

        private int GetWeekNumber(DateTime? date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNo = currentCulture.Calendar.GetWeekOfYear(
                            Convert.ToDateTime(date),
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);
            return weekNo;
        }



    }
}
