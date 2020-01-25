﻿using AutoMapper;
using BulbaCourses.PracticalMaterialsTests.Data.Models.Test.AnswerVariants;
using BulbaCourses.PracticalMaterialsTests.Data.Models.Test.Questions;
using BulbaCourses.PracticalMaterialsTests.Data.Models.Test;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.Test.AnswerVariants;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.Test.Questions;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.Test;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.User;
using BulbaCourses.PracticalMaterialsTests.Data.Models.User;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.WorkWithResultTest;
using BulbaCourses.PracticalMaterialsTests.Data.Models.WorkWithResultTest;

namespace BulbaCourses.PracticalMaterialsTests.Logic.AutoMapper.Profiles
{    
    public class AutoMapperProfile_DataLogicLayer : Profile
    {
        public AutoMapperProfile_DataLogicLayer()
        {
            // ----------- Test

            CreateMap<MTest_MainInfo, MTest_MainInfoDb>().ReverseMap();
            CreateMap<MReaderChoice_MainInfo, MReaderChoice_MainInfoDb>().ReverseMap();


            // ----------- Question

            CreateMap<MQuestion_ChoosingAnswerFromList, MQuestion_ChoosingAnswerFromListDb>().ReverseMap();            
            CreateMap<MQuestion_SetOrder, MQuestion_SetOrderDb>().ReverseMap();

            CreateMap<MReaderChoice_ChoosingAnswerFromList, MReaderChoice_ChoosingAnswerFromListDb>().ReverseMap();
            CreateMap<MReaderChoice_SetOrder, MReaderChoice_SetOrderDb>().ReverseMap();

            // ----------- AnswerVariant

            CreateMap<MAnswerVariant_ChoosingAnswerFromList, MAnswerVariant_ChoosingAnswerFromListDb>().ReverseMap();
            CreateMap<MAnswerVariant_SetOrder, MAnswerVariant_SetOrderDb>().ReverseMap();

            CreateMap<MReaderChoice_ChoosingAnswerFromList, MReaderChoice_ChoosingAnswerFromListDb>().ReverseMap();
            CreateMap<MReaderChoice_SetOrder, MReaderChoice_SetOrderDb>().ReverseMap();

            // ------------ User

            CreateMap<MUser_TestAuthor, MUser_TestAuthorDb>().ReverseMap();
            CreateMap<MUser_TestReader, MUser_TestReaderDb>().ReverseMap();

        }
    }
}
