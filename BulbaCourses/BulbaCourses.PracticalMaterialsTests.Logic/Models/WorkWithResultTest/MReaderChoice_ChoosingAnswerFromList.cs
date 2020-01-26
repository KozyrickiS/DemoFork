﻿using BulbaCourses.PracticalMaterialsTests.Data.Models.User;

namespace BulbaCourses.PracticalMaterialsTests.Logic.Models.WorkWithResultTest
{
    public class MReaderChoice_ChoosingAnswerFromList
    {
        public int Id { get; set; }

        // ------------ ReaderChoice

        public int ReaderChoice_MainInfoDb_Id { get; set; }

        public MReaderChoice_MainInfo ReaderChoice_MainInfo { get; set; }

        // ------------ Test        

        public int Question_ChoosingAnswerFromList_Id { get; set; }

        public int AnswerVariant_ChoosingAnswerFromList_Id { get; set; }
    }
}
