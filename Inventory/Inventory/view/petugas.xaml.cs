﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Inventory.view
{
    /// <summary>
    /// Interaction logic for petugas.xaml
    /// </summary>
    public partial class petugas : Window
    {
        private string proses;
        private Boolean hasil;
        private controller.PetugasController controller;

        /*
        ###### CONSTRUCTOR ######
        */


        public petugas()
        {
            InitializeComponent();
            controller = new controller.PetugasController(this);
            tampilData();
        }

        /*
        ###### OPERASI ######
        */

        //operasi tampil data
        public void tampilData()
        {
            controller.selectPetugas();
            aturButton(true);
        }

        //operasi tambah
        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            proses = "INSERT";
            txtIdPetugas.Text = "TIDAK BISA DIEDIT";
            txtIdPetugas.IsReadOnly = true;
            aturButton(false);
        }

        //operasi edit
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdPetugas.Text == "" || txtIdPetugas.Text == "ID")
            {
                MessageBox.Show("MOHON ISI ID PETUGAS", "ERROR!");
            }
            else
            {
                proses = "UPDATE";
                aturButton(false);
                List<string> dataPetugas = new List<string>();
                dataPetugas = controller.GetDataUpdateById();
                txtIdPetugas.Text = dataPetugas[0];
                txtIdPetugas.IsReadOnly = true;
                txtNamaPetugas.Text = dataPetugas[1];
                txtPassswordPetugas.Password = dataPetugas[2];
                txtStatusPetugas.Text = dataPetugas[3];
            }
        }

        //operasi hapus
        private void btnHapus1_Click(object sender, RoutedEventArgs e)
        {
            txtNamaPetugas.Text = "TIDAK BISA DIEDIT";
            txtNamaPetugas.IsReadOnly = true;
            txtPassswordPetugas.Password = "";
            txtPassswordPetugas.IsEnabled = false;
            txtStatusPetugas.Text = "";
            txtStatusPetugas.IsReadOnly = true;
            txtIdPetugas.Text = "";
            txtIdPetugas.Focus();
            proses = "DELETE";
            aturButton(false);
        }

        //operasi batal
        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            aturButton(true);
        }

        //operasi simpan
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (proses == "INSERT")
            {

                if (!checkNull()) hasil = controller.insertPetugas();
                clearAll();
            }
            else if (proses == "UPDATE")
            {
                if (!checkNull()) hasil = controller.updatePetugas();
                clearAll();

            }
            else if (proses == "DELETE")
            {
                hasil = controller.deletePetugas();
                clearAll();

            }
            if (hasil == true)
            {
                MessageBox.Show("Berhasil!");
            }
            else
            {
                MessageBox.Show("Gagal Menyimpan");
            }
            tampilData();
        }

        /*
        ###### NAVIGASI ######
        */

        //navigasi logout
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Window1 loginPage = new Window1();
            loginPage.Show();
            this.Hide();
        }

        //navigasi databarang
        private void btnBarang1_Click(object sender, RoutedEventArgs e)
        {
            DataBarang data = new DataBarang();
            data.Show();
            this.Close();
        }

        //navigasi databarang
        private void btnDataBarang1_Click(object sender, RoutedEventArgs e)
        {
            DataBarang data = new DataBarang();
            data.Show();
            this.Close();
        }

        //navigasi beranda
        private void btnBeranda_Click(object sender, RoutedEventArgs e)
        {
            view.MenuUtama menu = new view.MenuUtama();
            menu.Show();
            this.Close();
        }

        /*
        ###### HELPER ######
        */

        //metode untuk atur button
        public void aturButton(Boolean status)
        {
            btnTambah.IsEnabled = status;
            btnEdit.IsEnabled = status;
            btnHapus.IsEnabled = status;
            btnSave.IsEnabled = !status;
            btnBatal.IsEnabled = !status;
        }

        //metode untuk check kekosongan form
        private Boolean checkNull()
        {
            bool kosong;
            kosong = false;
            if (txtIdPetugas.Text == "" || txtNamaPetugas.Text == "" || txtPassswordPetugas.Password == "" || txtStatusPetugas.Text == "")
            {
                MessageBox.Show("ANDA HARUS MENGISI SEMUA FIELD!", "ERROR!");
                kosong = true;
            }
            return kosong;
        }

        //metode untuk menghapus semua data form
        private void clearAll()
        {
            txtIdPetugas.IsReadOnly = false;
            txtIdPetugas.Text = "";
            txtNamaPetugas.IsReadOnly = false;
            txtNamaPetugas.Text = "";
            txtPassswordPetugas.Password = "";
            txtPassswordPetugas.IsEnabled = true;
            txtStatusPetugas.Text = "";
            txtStatusPetugas.IsReadOnly = false;
        }
    }
}
