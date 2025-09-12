// CleanSchoolProgram.cs
// Quản lý trường học - Clean Code version

using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanSchoolProgram
{
    // --- Entity classes ---
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public double GPA { get; set; }

        public override string ToString()
        {
            return $"ID:{Id} | Name:{Name} | Age:{Age} | GPA:{GPA}";
        }
    }

    // --- Service class ---
    public class StudentService
    {
        private readonly List<Student> students = new();

        public void AddStudent()
        {
            Console.Write("ID: "); string id = Console.ReadLine();
            Console.Write("Tên: "); string name = Console.ReadLine();
            Console.Write("Tuổi: "); int age = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("GPA: "); double gpa = double.Parse(Console.ReadLine() ?? "0");

            students.Add(new Student { Id = id, Name = name, Age = age, GPA = gpa });
        }

        public void RemoveStudent()
        {
            Console.Write("Nhập ID cần xóa: ");
            string id = Console.ReadLine();
            var st = students.FirstOrDefault(s => s.Id == id);
            if (st != null)
            {
                students.Remove(st);
                Console.WriteLine("Đã xóa.");
            }
            else Console.WriteLine("Không tìm thấy.");
        }

        public void UpdateStudent()
        {
            Console.Write("Nhập ID cần cập nhật: ");
            string id = Console.ReadLine();
            var st = students.FirstOrDefault(s => s.Id == id);
            if (st == null) { Console.WriteLine("Không tìm thấy."); return; }

            Console.Write("Tên mới: "); st.Name = Console.ReadLine();
            Console.Write("Tuổi mới: "); st.Age = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("GPA mới: "); st.GPA = double.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Đã cập nhật.");
        }

        public void ShowAll()
        {
            if (students.Count == 0) Console.WriteLine("Danh sách rỗng.");
            else students.ForEach(Console.WriteLine);
        }

        public void FindByName()
        {
            Console.Write("Nhập tên: ");
            string name = Console.ReadLine();
            var result = students.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            foreach (var s in result) Console.WriteLine(s);
        }

        public void ShowExcellent()
        {
            var result = students.Where(s => s.GPA > 8);
            foreach (var s in result) Console.WriteLine("Sinh viên giỏi: " + s);
        }

        public void SortByName() => students.Sort((a, b) => a.Name.CompareTo(b.Name));
        public void SortByGPA() => students.Sort((a, b) => b.GPA.CompareTo(a.GPA));
    }

    // --- Main Program ---
    class Program
    {
        static void Main()
        {
            StudentService studentService = new();
            int choice;
            do
            {
                Console.WriteLine("\n=== MENU CHÍNH ===");
                Console.WriteLine("1. Quản lý Sinh viên");
                Console.WriteLine("99. Thoát");
                Console.Write("Chọn: ");
                choice = int.Parse(Console.ReadLine() ?? "0");

                if (choice == 1) ShowStudentMenu(studentService);

            } while (choice != 99);
        }

        static void ShowStudentMenu(StudentService service)
        {
            int smenu;
            do
            {
                Console.WriteLine("\n--- QUẢN LÝ SINH VIÊN ---");
                Console.WriteLine("1. Thêm SV");
                Console.WriteLine("2. Xóa SV");
                Console.WriteLine("3. Cập nhật SV");
                Console.WriteLine("4. Hiển thị tất cả");
                Console.WriteLine("5. Tìm theo tên");
                Console.WriteLine("6. SV GPA > 8");
                Console.WriteLine("7. Sắp xếp theo tên");
                Console.WriteLine("8. Sắp xếp theo GPA");
                Console.WriteLine("9. Quay lại");
                Console.Write("Chọn: ");
                smenu = int.Parse(Console.ReadLine() ?? "0");

                switch (smenu)
                {
                    case 1: service.AddStudent(); break;
                    case 2: service.RemoveStudent(); break;
                    case 3: service.UpdateStudent(); break;
                    case 4: service.ShowAll(); break;
                    case 5: service.FindByName(); break;
                    case 6: service.ShowExcellent(); break;
                    case 7: service.SortByName(); Console.WriteLine("Đã sắp xếp theo tên."); break;
                    case 8: service.SortByGPA(); Console.WriteLine("Đã sắp xếp theo GPA."); break;
                }

            } while (smenu != 9);
        }
    }
}
