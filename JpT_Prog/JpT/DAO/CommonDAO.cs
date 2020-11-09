using JpT.Entity;
using JpT.Model;
using JpT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JpT.DAO
{
    public class CommonDAO
    {
        private static ExcelUtils _excel = new ExcelUtils(Constant.FILE_DATA);
        private static List<DataEntity> _listData = new List<DataEntity>();
        private static List<LessonModel> _listLesson = new List<LessonModel>();
        private static List<string> _listLastLearning = new List<string>();

        public static List<DataEntity> GetAllEntity(TabName tabname)
        {
            if (tabname == TabName.Kanji)
            {
                return _listData.Where(x => x.Type.Equals("KJ")).ToList();
            }
            else
            {
                return _listData.Where(x => x.Type.Equals("TV")).ToList();
            }
        }

        public static List<string> GetListLastLearning(TabName tabname)
        {
            return _listLastLearning;
        }

        public static List<LessonModel> GetListLesson(TabName tabname)
        {
            if (tabname == TabName.Kanji)
            {
                return _listLesson.Where(x => x.Type.Equals("KJ")).ToList();
            }
            else
            {
                return _listLesson.Where(x => x.Type.Equals("TV")).ToList();
            }
        }

        public static List<DataEntity> GetDataByLesson(TabName tabname, string lesson = "")
        {
            if (tabname == TabName.Kanji)
            {
                return _listData.Where(x => x.Lesson.Equals(lesson) && x.Type.Equals("KJ")).ToList();
            }
            else
            {
                return _listData.Where(x => x.Lesson.Equals(lesson) && x.Type.Equals("TV")).ToList();
            }
        }

        public static void UpdateDataIsHardAndLock(List<DataEntity> listDataEntity)
        {
            _excel.ws_GetBySheetIndex(0);

            foreach (DataEntity entity in listDataEntity)
            {
                _excel.cell_WriteByIndex(entity.Id, Constant.COL_IS_HARD, entity.Hard);
                _excel.cell_WriteByIndex(entity.Id, Constant.COL_LOCK, entity.Lock);

                int index = _listData.IndexOf(entity);

                if (index != -1)
                {
                    _listData[index].Hard = entity.Hard;
                    _listData[index].Lock = entity.Lock;
                }
            }
            _excel.Workbook_Save(Constant.FILE_DATA);
        }

        public static List<DataEntity> GetWasLearned(TabName tabname)
        {
            if (tabname == TabName.Kanji)
            {
                return _listData.Where(x => (!string.IsNullOrEmpty(x.LastLearn) && x.Type.Equals("KJ"))).ToList();
            }
            else
            {
                return _listData.Where(x => (!string.IsNullOrEmpty(x.LastLearn) && x.Type.Equals("TV"))).ToList();
            }
        }

        public static bool InitData()
        {
            try
            {
                string listKotobaStr = string.Empty;
                _excel.ws_GetBySheetIndex(0);
                int countRows = _excel.ws_GetCountRow();
                for (int i = 3; i < countRows; i++)
                {
                    DataEntity entity = new DataEntity();
                    entity.Id = i;
                    entity.Type = _excel.cell_GetValueByCell(i, Constant.COL_TYPE);
                    entity.Level = _excel.cell_GetValueByCell(i, Constant.COL_LEVEL);
                    entity.Lesson = _excel.cell_GetValueByCell(i, Constant.COL_LESSON);
                    entity.Kanji = _excel.cell_GetValueByCell(i, Constant.COL_KANJI);
                    entity.Hiragana = _excel.cell_GetValueByCell(i, Constant.COL_HIRAGANA);
                    entity.CnVi = _excel.cell_GetValueByCell(i, Constant.COL_CNVI);
                    entity.Mean = _excel.cell_GetValueByCell(i, Constant.COL_MEANING);
                    entity.Hard = _excel.cell_GetValueByCell(i, Constant.COL_IS_HARD);
                    entity.Lock = _excel.cell_GetValueByCell(i, Constant.COL_LOCK);
                    entity.LastLearn = _excel.cell_GetValueByCell(i, Constant.COL_LAST_LEARN);
                    if (entity.isEmpty())
                    {
                        continue;
                    }

                    _listData.Add(entity);

                    if (!listKotobaStr.Contains(entity.Lesson))
                    {
                        listKotobaStr += entity.Lesson;
                        _listLesson.Add(new LessonModel() { LessonName = entity.Lesson, LastLearning = entity.LastLearn, Type = entity.Type, IsSelected = false });
                        _listLastLearning.Add(entity.LastLearn);
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
