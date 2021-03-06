﻿using DataBuilders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;

namespace HETSPrism.Services
{
    public class HomeExercisesParser
    {
        private string _folderPath;
        public string ExtractPath { get; set; }

        public ObservableCollection<HomeExercise> HomeExercises { get; set; }
        public HomeExercisesParser(string folderPath)
        {
            _folderPath = folderPath;
            HomeExercises = new ObservableCollection<HomeExercise>();
        }

        public void TraverseTree()
        {
            bool extracted = false;
            Stack<string> dirs = new Stack<string>();

            if (!System.IO.Directory.Exists(_folderPath))
            {
                throw new ArgumentException();
            }
            dirs.Push(_folderPath);


            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                   
                    try
                    {
                        if (file.EndsWith(".zip") && !extracted)
                        {
                            ExtractZipFile(file);
                            dirs.Push(currentDir);
                            extracted = true;
                        }else if (file.EndsWith(".java") || file.EndsWith(".jar"))
                        {
                            Console.WriteLine();
                            CreateHomeExercise(file);
                            extracted = false;
                        }
                        else
                        {
                            extracted = false;
                        }
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                  
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }

        private void CreateHomeExercise(string fileName)
        {
            bool isMain = false;
            if (fileName.EndsWith(".java"))
            {
                isMain = IsMainFile(fileName);
            }

            if (isMain || fileName.EndsWith(".jar"))
            {
                string fileID = new DirectoryInfo(Path.GetDirectoryName(fileName)).Name;
                string folderPath = Path.GetDirectoryName(fileName) + "\\";
                string name = Path.GetFileName(fileName);
                var homeExercise = new HomeExercise()
                { HomeExercisePath = folderPath, HomeExerciseFolderName = fileID, HomeExerciseName = name };
                HomeExercises.Add(homeExercise);
            }
        }

        private void ExtractZipFile(string filePath)
        {
            ExtractPath = Path.GetDirectoryName(filePath);
            try 
            {
                ZipFile.ExtractToDirectory(filePath, ExtractPath, CodePagesEncodingProvider.Instance.GetEncoding("DOS-862"), true);
                File.Delete(filePath);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + $"in file {filePath}");
            }  
        }

        private bool IsMainFile(string filePath)
        {
            bool isMain = false;
            using (StreamReader sr = File.OpenText(filePath))
            {
                string text = sr.ReadToEnd();
                if (text.Contains("main"))
                {
                    isMain = true;
                }
            }
            return isMain;
        }
    }
}
