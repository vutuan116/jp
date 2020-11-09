using JpT.DAO;
using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JpT.Logic
{
    public class ViewKanjiFlashcard
    {
        public ObservableCollection<LessonModel> GetListLesson(TabName tabname)
        {
            ObservableCollection<LessonModel> result = null;
            result = new ObservableCollection<LessonModel>(CommonDAO.GetListLesson(tabname));
            foreach (LessonModel lesson in result)
            {
                if (string.IsNullOrEmpty(lesson.LastLearning))
                {
                    lesson.IsSelected = true;
                    break;
                }
            }
            return result;
        }

        public List<DataModel> GetListObjByLesson(TabName tabname, LessonModel lesson, bool isOnlyHard = false, bool isGetAll = false)
        {
            List<DataModel> result = new List<DataModel>();
                List<DataEntity> listEntity = CommonDAO.GetDataByLesson(tabname, lesson.LessonName);

                foreach (DataEntity entity in listEntity)
                {
                    DataModel model = CommonUtils.MappingData<DataModel>(entity);
                    model.IsHard = !string.IsNullOrEmpty(entity.Hard);
                    model.IsLock = !string.IsNullOrEmpty(entity.Lock);

                    if (isGetAll)
                    {
                        result.Add(model);
                    }
                    else if (isOnlyHard)
                    {
                        if (model.IsHard) result.Add(model);
                    }
                    else if (!model.IsLock)
                    {
                        result.Add(model);
                    }
                }
          
            return result;
        }

        //public void UpdateLastLearning(TabName tabName, ObservableCollection<LessonModel> lessonList)
        //{
        //    List<LessonModel> data = new List<LessonModel>(lessonList);
        //}

        public void SaveListHard(TabName tabName, ObservableCollection<DataModel> objList)
        {
            if (tabName == TabName.Kanji)
            {
                List<DataEntity> data = new List<DataEntity>();
                foreach (DataModel model in objList)
                {
                    DataEntity entity = new DataEntity() { Id = model.Id, Hard = model.IsHard ? "1" : string.Empty, Lock = model.IsLock ? "1" : string.Empty };
                    data.Add(entity);
                }
                CommonDAO.UpdateDataIsHardAndLock(data);
            }
        }
    }
}
