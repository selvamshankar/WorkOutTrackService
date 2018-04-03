using System;
using System.Collections.Generic;
using System.Linq;
using WorkOutDBModel.Model;

namespace WorkOutDBLayer
{
    public class WorkOutDb
    {
        WorkOutDBLayer.MyContext db = null;
        public WorkOutDb()
        {
            db = new WorkOutDBLayer.MyContext();
        }

        public List<WorkOut_VM> GetAll()
        {
            try
            {
                List<WorkOut_VM> results = new List<WorkOut_VM>();

                List<WorkOut> workouts = db.WorkOutCollection.ToList();

                foreach (WorkOut workout in db.WorkOutCollection.ToList())
                {
                    Workout_Active workoutactive = db.WorkOutsActive.Where(a => a.WorkOutId == workout.WorkOutId && a.End_Date == null).FirstOrDefault();

                    if (workoutactive != null)
                    {
                        results.Add(new WorkOut_VM
                        {
                            WorkOutId = workoutactive.WorkOutId,
                            Id = workoutactive.Id,
                            WorkOutTitle = workout.WorkOutTitle,
                            StartDate = workoutactive.Start_Date.ToString(),
                            EndDate = workoutactive.End_Date.ToString(),
                            WorkOutComment = workoutactive.Comment
                        });
                    }
                    else
                    {
                        results.Add(new WorkOut_VM
                        {
                            WorkOutId = workout.WorkOutId,
                            WorkOutTitle = workout.WorkOutTitle,
                            Id = 0,
                        });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                return new List<WorkOut_VM>();
            }
        }


        public List<WorkOut_VM> GetAllByTitle(string Title)
        {
            try
            {

                List<WorkOut_VM> results = GetAll().Where(a => a.WorkOutTitle.ToLower().Contains(Title.ToLower())).ToList();

                return results;
            }
            catch (Exception ex)
            {
                return new List<WorkOut_VM>();
            }
        }


        public WorkOut GetById(int Id)
        {
            try
            {
                return db.WorkOutCollection.Find(Id);
            }
            catch (Exception ex)
            {
                return new WorkOut();
            }
        }

        public bool Add(WorkOut model)
        {
            try
            {
                model.Created = DateTime.Now;
                db.WorkOutCollection.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Update(WorkOut model)
        {
            try
            {
                WorkOut editWorkout = db.WorkOutCollection.Find(model.WorkOutId);
                if (editWorkout != null)
                {
                    editWorkout.WorkOutTitle = model.WorkOutTitle;
                    editWorkout.WorkOutNote = model.WorkOutNote;
                    editWorkout.CategoryId = model.CategoryId;
                    model.Updated = DateTime.Now;
                    db.Entry(editWorkout).CurrentValues.SetValues(model);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int Id)
        {
            try
            {
                WorkOut WorkOut = db.WorkOutCollection.Find(Id);
                db.WorkOutCollection.Remove(WorkOut);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



    }
}
