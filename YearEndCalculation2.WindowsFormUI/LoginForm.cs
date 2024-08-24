using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation.Business.Tools;
using YearEndCalculation.WindowsFormUI.Properties;
using YearEndCalculation2.WindowsFormUI;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class LoginForm : Form
    {
        DateTime currentDateTime = new DateTime();
        List<PhysicalAddress> physicalAddressList = new List<PhysicalAddress>();
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "MkysTdmsLisans.txt");
        int keyIndex = 0;

        public LoginForm()
        {
            InitializeComponent();

        }


        private void btnSerial_Click(object sender, EventArgs e)
        {
            
            if (mtbxSerial.Text.ToUpper() == createPass())
            {
                MessageBox.Show("Teşekkür ederiz iyi çalışmalar.");
                if (physicalAddressList.Count > 0)
                {
                    Properties.LicenceSetting.Default.Licenced = true;
                    Properties.LicenceSetting.Default.Save();
                    string cryptedLicenceKey = Crypt.Encrypt(physicalAddressList[0].ToString());
                    File.WriteAllText(path, cryptedLicenceKey);

                }
                FormMain formMain = new FormMain();
                formMain.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçersiz anahtar. Lütfen kontrol ediniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private string createPass()
        {
            //int keyTime = (currentDateTime.Year * 372 + currentDateTime.Month * 31 + currentDateTime.Day) * 24 + currentDateTime.Hour;
            //int keyIndex = keyTime % 500;

            string[] keys = new string[] {
                "6bd6-a421-4f2b-a3ea-b96d",
"08f6-6c2b-45e1-b501-1465",
"e75d-02b1-42f2-8ee0-c3bc",
"9418-355c-4da8-af29-f779",
"beca-0b1c-4332-8353-9e2b",
"2341-447c-4421-a05b-bf13",
"0175-56eb-41f4-9838-3d5e",
"d8f4-9c29-40ef-8a52-8277",
"7df2-4dce-4869-9d28-0a65",
"2a56-1169-4af5-a075-f88c",
"9b8c-f303-4a31-82ee-f320",
"d26c-02ad-45fb-add9-c21d",
"099b-e840-42df-b52d-df79",
"f0b2-c438-4655-a2f8-0c7a",
"d7c1-ef84-4527-8678-8d94",
"54c1-07fb-42b4-b9f3-d28d",
"8924-3ae1-4769-982b-62fb",
"b74f-b532-474a-a597-de76",
"95cd-3806-4a30-899c-52c1",
"cb5e-3e7c-44e5-87de-c335",
"71f0-7fed-4bcb-aa81-b93d",
"08d0-2948-49ac-934d-eda0",
"cb01-50d9-4d93-9271-e866",
"8ab3-e784-4c12-925b-05f9",
"a76e-b4fd-47e3-871a-d910",
"e6c4-7dda-4812-b7bf-d43f",
"6780-1224-428f-b83d-1ee8",
"06e5-0b27-4a6c-a85e-b08a",
"c19d-9e15-41a1-aca6-a1a8",
"8841-5838-472e-b534-64d0",
"22a6-7e11-464d-9ce2-496c",
"7d3a-8730-4d9f-b396-8dd1",
"bab9-c54f-4b7b-86ba-d14d",
"85b9-fa1f-42bc-b9d5-2788",
"09e4-e2d5-4793-ab30-9a33",
"3fad-a78d-460e-811d-95c3",
"49cd-453e-4e8d-97d9-84ce",
"e07e-f86f-4330-94d1-6b73",
"fbda-4e4e-4d0b-a8da-0066",
"23b4-916a-4d93-a2bf-83b9",
"1e34-7b9d-4490-a74f-905e",
"1665-6ed2-40c6-a6ff-d513",
"d4c5-501f-4b19-a57a-48f1",
"3979-4d25-4182-8bff-a348",
"731e-e405-4243-bfa4-3b8a",
"5598-2790-4a54-8732-197a",
"e4ea-71cf-48ae-95f1-4d79",
"5958-9069-42f1-bed0-6db2",
"34f4-e477-4f82-806f-1a07",
"097f-3e82-4fa2-a20c-a278",
"5018-ae86-48c3-b163-9e3e",
"f498-7226-4065-ae5f-a610",
"8e8b-4d69-44c1-9d2d-62a8",
"daa5-3670-4b2d-9d56-e584",
"a67f-d6d7-4f3b-8648-af5b",
"797e-91d6-40c7-a857-5ce1",
"3140-e707-4d19-ae7d-32a3",
"c835-a852-44bb-8b67-394b",
"7adf-4c8b-4786-9b5a-0203",
"a29b-54a5-49d2-ae62-d225",
"5026-1ea6-498b-a23e-b839",
"99c8-1725-456e-8019-6505",
"f9f5-c5b3-4d21-8b64-9a02",
"dbd7-0d9e-44f5-af7b-4078",
"5c45-01b7-43d2-8c83-63df",
"975f-e582-442f-a91f-52c0",
"9c6d-6422-421f-8909-78a9",
"5c69-242e-4f29-90e7-f2ba",
"32c8-9ce3-4e76-9cb1-1350",
"31be-1e27-4ea5-9a2c-851b",
"b941-755d-41bc-8127-d21e",
"2a90-7dd9-45ea-b329-cbbf",
"c698-5310-4fa9-9309-e840",
"5453-c226-4fd4-a602-1566",
"9ea9-cda2-45e0-b9c3-fe1c",
"a000-d9f6-47ac-87ad-93db",
"e974-e4bb-49d9-b57a-6aca",
"5ca2-99ac-46d8-ad0a-c679",
"9358-6528-4fb0-85dc-af61",
"8697-45f7-44a9-a2db-f2a2",
"8be3-9f8d-45ec-b43a-7def",
"a672-ddee-4b69-a328-d4ff",
"929f-e649-434c-971c-354f",
"dd2b-76a1-451f-810d-85a7",
"d80d-73d2-475f-903c-c319",
"0f17-36c0-4414-b8fa-e907",
"f0b7-acfe-454f-a17e-d478",
"691f-1088-491e-b39f-d818",
"505e-2c99-4770-9fc7-3a02",
"61a2-faae-4bb2-9b27-ed10",
"2bbe-6787-4fe2-aa35-4f0e",
"fc4c-bab2-4bae-894f-28b1",
"6c1a-6665-4db0-b832-3aaf",
"8432-e57b-4f9f-82e5-c5d1",
"ac06-8a0a-47b7-9e63-30fe",
"3427-37ad-4ad5-b9dc-6bfa",
"413c-5792-42a8-8b21-2174",
"adbe-e471-4207-bdbf-db29",
"fc95-8ab2-4852-ae78-74a2",
"8e5f-daf7-44cb-96fc-bdaa",
"2c18-4514-4cee-b61a-4c6e",
"4309-325d-482e-aa07-9629",
"baa3-069a-438a-9ad0-815b",
"7223-28d4-4517-b523-fec8",
"959b-29a0-423c-8bbf-10bc",
"02a9-acdf-4125-b829-6833",
"20e1-14d8-407a-8137-4d3d",
"670e-c524-454c-90a8-2a68",
"8d83-775e-451a-8d47-3083",
"3e2e-f15c-4841-9ec6-3a74",
"9353-19ee-4052-8b15-dd85",
"73af-4130-40f6-ac54-bb38",
"86fa-269f-4f9e-8715-8722",
"d5ee-35f8-4afe-8202-aa43",
"d115-b77b-418a-b766-d967",
"e0ee-6367-4838-980d-ecff",
"c929-3d07-4fc5-b7bb-f25b",
"6ff2-b34f-4f2e-ba24-0fe5",
"49aa-ab8a-4240-bc68-d209",
"f85f-7150-4d52-a4ea-2117",
"b72f-0793-419f-a7ff-e3d8",
"d618-76ff-4b68-84a3-fd3a",
"86d8-0314-4ef5-971d-6101",
"20b2-01d7-4b57-9b24-c0d2",
"9311-a321-4ab4-989f-fe97",
"598c-a03d-4fa4-9e0e-9122",
"c4b4-5a2d-44e8-8b88-1197",
"6319-1381-404f-adbd-0208",
"f67d-9478-4e07-9e7c-6c23",
"8d4f-0115-4abd-a8ad-93b9",
"db49-5c43-4cda-b6ad-bbeb",
"fdc7-c063-467a-a230-94ca",
"f581-1e32-452f-8878-30cd",
"f2c7-e6d6-43cb-a815-e317",
"3d94-be68-49a0-89bd-3ce9",
"406e-ed7d-4114-b10b-825f",
"f7df-fa68-4321-982d-3137",
"e29e-fc3c-4eb4-81f6-7b65",
"8433-2a11-4de2-a097-ef8a",
"dd33-ff58-409d-8bbc-ff0f",
"d43e-66cd-4ce8-a015-8420",
"1585-6a22-4bf4-ae18-191b",
"5aa0-8b67-4b03-a6bb-1214",
"c4be-2ddc-42c4-8e2c-cd53",
"e894-7a0a-403e-8741-edb6",
"b18b-03dd-466d-bee7-0d37",
"8880-916e-4673-b329-7c4b",
"3278-351d-440d-a6ea-5376",
"960f-1b28-4730-9e1d-6b76",
"f799-7b71-4025-8eb0-011e",
"cf77-32f5-4269-9515-0f75",
"8846-9ec7-44bb-9d57-763a",
"2f01-59fd-4198-91d6-067a",
"5e46-b8a4-4b57-acc2-934a",
"8ea3-d7dc-401b-90cb-c09f",
"a052-8ff9-4a06-87da-7f56",
"5c38-1169-4a99-888a-485e",
"7aee-5f2b-423b-84a4-5339",
"0484-8943-48df-ae4d-0894",
"14ae-e259-4464-a43e-1d9c",
"4af2-d556-43aa-a658-0047",
"0dcb-d7e7-4c84-b128-aa27",
"6e5f-4d10-40fa-8dbe-879c",
"88be-ecaa-4227-befc-b882",
"e6b6-43bf-4403-b201-ef47",
"97d6-7ccd-49e3-8ee5-0a1c",
"7526-7c03-4561-9652-83c9",
"c40e-458f-4bc5-982e-04b0",
"d830-f42e-4b05-b5ea-55f5",
"281f-1797-476f-a41f-3da4",
"3231-846f-4f94-b6af-50ca",
"f438-e12d-4216-8ecf-4434",
"5dbb-6af7-4a1c-a005-1b52",
"6c85-1af8-40f3-9865-8731",
"01cf-7580-4df6-a9fc-1f12",
"b781-d5aa-47cb-94bc-1c29",
"8173-279c-4c29-ad2e-bb14",
"98f6-4359-41e3-b006-8fac",
"574f-6abd-48bb-b7d6-67d7",
"6585-7330-4f82-899f-e1af",
"430a-ab65-46ad-93cf-025e",
"e4c8-6c2e-4ff2-86ab-6ce0",
"4053-b88c-4cb1-843d-9ce6",
"f7b5-1fa7-42cc-94c0-7107",
"5cd9-555b-415c-b485-85aa",
"31d5-1965-49ae-ac96-4cc1",
"c974-eb8c-4499-8c3b-9a98",
"ba7f-bfa4-4c2f-ba30-220e",
"7204-3611-40b8-8e87-7a99",
"d6cd-b576-4e2d-a1a9-6a87",
"a51b-ebee-4bed-8b8d-30aa",
"b260-a4d1-4390-9efa-e16d",
"00dc-2d2c-4eba-a6e2-35d8",
"d9b8-0fc7-4f4a-9403-7b10",
"8729-eef1-4a88-8145-3dcd",
"252f-4947-47a5-b0b2-ee1a",
"ec36-913b-4afa-8331-19bb",
"a45b-4c25-4c6f-8853-f7ba",
"2465-c4b9-42d1-ac99-b4da",
"cf9c-ab0f-46b4-8190-3b2a",
"377b-4b02-4f10-858f-3c43",
"141d-f438-4ca0-9dea-bad2",
"078c-4d59-4b95-806c-8236",
"f0b8-3e13-400d-a584-94ef",
"b558-b90f-442b-8770-2ab2",
"9a0a-0c43-4499-b709-b429",
"95c3-2ca8-4bb6-8200-cbfb",
"a812-c3cd-4991-aded-ed7d",
"d858-fa55-4ec8-97fd-a078",
"56fc-b2da-45de-a1db-2525",
"f879-6da6-4d61-bf06-f457",
"d54d-2d90-4d0e-a710-0c08",
"436c-2024-4913-8c58-742e",
"954d-6882-4df1-b1bf-dce4",
"0230-9f0a-425f-b04c-8483",
"6b38-551b-49b4-961d-dfbd",
"bfa9-35d6-4361-af4f-b4a2",
"1034-7129-42fc-8a05-ebf6",
"1186-ad1a-4e2a-bed9-f1cd",
"59a7-d61a-4ef6-8106-ae51",
"1033-f341-4774-a701-0132",
"014a-fe91-4dc8-8c51-a9d9",
"aba0-c06d-4ec5-9324-b6d7",
"b558-5314-4ee1-bbfe-f7e0",
"3920-0e59-4d38-96cb-f779",
"ed05-092b-4dff-b81a-550a",
"b996-5b23-4ca7-8497-9c33",
"a665-3f55-4f89-bb62-8cc2",
"7618-2090-463e-905e-e24b",
"286b-5003-47e4-9827-2cb3",
"b8f9-4538-43b5-b457-8723",
"1aa8-5bac-4a10-9ba0-95ca",
"52d0-c477-4f8b-b8d5-bee1",
"c14d-66cc-4f86-ac3a-9eef",
"c577-9880-4dc6-b96d-6bf8",
"72df-d906-4e41-8780-98df",
"4ab2-9a46-4cd5-b702-97b7",
"2539-89e5-4e46-9b19-9240",
"7271-a484-4a7a-8a51-b6d3",
"41b5-1bd8-485f-bdf0-435a",
"b45a-a45d-4ba7-89a2-da76",
"91f0-e10f-4063-b32c-0988",
"0cd9-2724-4679-9b21-d862",
"b9e1-9385-4fae-b826-1e7e",
"b322-ca28-461d-9341-7ceb",
"1e84-861b-4c38-8b75-40f2",
"87d7-4672-414a-beee-1bd9",
"dd66-613b-4dea-9c2b-54c0",
"e89f-e19b-4cb3-823d-3c13",
"89a4-d627-4dd4-8cf7-4556",
"94a7-5f71-4639-b890-be7f",
"0d71-bdfe-47db-a651-9a51",
"657e-2758-497e-9bc5-9b85",
"84ed-72af-42ce-b9b2-b1eb",
"1c6d-950d-4585-91bd-8300",
"fbda-20bf-4a2b-9324-8cb0",
"0e72-3edd-4730-8062-ce5f",
"c550-e8c4-48bd-a3c5-1180",
"4d27-e1e9-4bc6-89ff-728c",
"a2ed-caf1-4c8b-9bbf-1ce4",
"c148-c8e3-46d6-b80e-c799",
"3017-008d-40f4-9393-b0fe",
"46bc-bf8b-41f5-a276-cea7",
"822a-e167-493f-8448-3bd9",
"9c1b-40b8-4d6a-bff6-532f",
"ae6e-4f18-43a7-9313-c770",
"3c10-39fa-414e-8466-0390",
"2313-12d3-4730-911a-9855",
"e19c-435b-4a02-985f-902e",
"7b24-8fa3-4422-b58a-92e2",
"d0ab-496b-46a9-ae90-00ba",
"c079-e600-4617-ae37-9d38",
"02df-74e6-4d9e-a93c-bfb4",
"b662-7d6b-41d3-987a-edce",
"d6fc-d413-481b-b2f0-d238",
"afab-feb9-4b2e-92e3-da61",
"6bef-2cd3-443f-8c01-d739",
"441e-9899-4af3-8e2e-4227",
"705e-21ce-49f2-b165-008e",
"3bec-bdd9-4404-bd35-0a45",
"696e-e949-45d1-b3cb-a7c2",
"72f4-349e-4966-9a9a-1360",
"c92b-cf58-4f02-a7b0-7462",
"e3f9-d555-498d-a3cf-8b67",
"a8bf-2fcf-41af-822e-d761",
"5052-866d-46fe-acbc-48ef",
"a502-faa4-41f8-9740-57cb",
"6358-33f8-46c3-9e08-1ffc",
"281d-18e1-466a-9ff9-9d34",
"1f73-1df9-47db-b6a9-6323",
"0e10-abe1-4df6-a926-e792",
"ca44-26d9-4268-ab89-7235",
"adbe-db9b-4da7-a9d4-7b14",
"b4e0-b828-422f-a98f-4b8e",
"3d90-a115-4ae3-90be-c820",
"0dce-04cd-4be7-803b-198f",
"38e7-a8b3-4846-8fb7-4d65",
"05c1-13d6-4fc9-bc43-4717",
"d070-8f57-4d7d-9bac-b9ac",
"b1b3-8456-4e8c-95b3-940a",
"bd54-f8e5-4433-aaae-d50b",
"08e9-a67b-43ce-898d-4457",
"f65b-a2d3-4815-9d18-a2a5",
"5e1e-cd97-4be2-9017-1c54",
"a3d9-2c55-4744-b59a-c93e",
"a4c9-b90a-42d1-97da-9199",
"3c51-c68c-4738-9004-cecd",
"1691-49ab-42ae-912d-1469",
"d588-7bbb-4945-859a-e778",
"a7ed-c091-472e-91bf-1d8a",
"04f9-592f-4a25-a43f-53a7",
"ba79-e6e5-4410-bff1-6fb5",
"037d-8c85-45e0-9884-0426",
"ca05-5c22-4277-b74e-22c8",
"77f2-b4af-41dc-b2cc-3909",
"d677-9e71-45dd-9346-4f45",
"1b1e-f9d3-4d28-9edc-0c80",
"ab6e-9c81-4e2d-a88c-5f4e",
"6c15-7bdf-4ecb-b690-3d13",
"5ff9-f86a-45a8-83d4-b905",
"d232-9420-4d6f-bd1d-cf50",
"46d4-7b70-4158-96b5-4e4a",
"1fee-cf65-4b1e-a6e7-7139",
"a733-dfc3-4f68-845f-e95d",
"4d69-1e4d-412c-8af0-db69",
"f810-3ddc-4bde-9ff1-713b",
"755b-9b7f-412a-9d6d-bbde",
"bffe-6cdf-44b3-8af0-b883",
"4aba-3d37-43ee-8723-5ec3",
"ac59-9129-4342-b833-b4c1",
"e96c-0827-48f3-aa2e-35ed",
"c396-c28d-482f-8bd8-4750",
"aebe-7b9a-482d-97f4-7d0b",
"3c43-d27b-4a90-82f6-0dc6",
"68ab-7c2e-4768-bcca-0336",
"9c4c-1fee-4bd6-890a-8ae6",
"bd3c-3bf9-48a6-99c9-3bc5",
"d35b-fe1a-46f4-944b-f5b8",
"1898-f7a1-4057-9dcd-b091",
"976e-16e4-47f3-ae3c-f9bb",
"af51-c2e8-43f2-809d-c26f",
"2c75-d223-4cda-bbf9-3038",
"cd38-a642-47c3-a6f3-aeb7",
"7913-b8a4-4460-b546-887d",
"859e-bdd8-43d5-b84d-d9dc",
"5b3f-27f0-4dd4-905c-33ae",
"2548-2dbf-44c8-a7a7-aa25",
"41e8-8536-49f3-83d5-3b7b",
"b30c-f1b7-4419-99e1-bc5c",
"01fd-0c48-4957-84f5-f6cb",
"6bc6-9bef-4479-98df-a7b3",
"8a27-462e-463e-8846-1681",
"e8ff-75c0-4954-90f0-7575",
"6cb1-142d-46ba-a3c5-0fa0",
"7abe-5abd-4d09-803d-acd6",
"2573-b879-4db2-af7f-da8f",
"2314-6669-49e1-b481-8e79",
"6ca2-5a28-4a38-985c-41f6",
"b24b-6769-441b-adbe-3830",
"2515-921d-4f81-b7d6-8329",
"09af-4df5-4933-a3ed-eed4",
"74e1-d609-4ec3-bb79-aad6",
"e19f-5f6d-449d-95e2-3028",
"0498-19c0-4ec4-b00c-38f7",
"361b-5731-4ab4-b7a3-94e9",
"17d6-b3c8-4b59-84b0-a89e",
"b56a-44b8-4593-b0cf-a38e",
"5efd-3076-41f0-8174-6587",
"ff4d-7e32-44d8-a372-a29e",
"32c2-034e-4a7b-a743-f4d9",
"0ef7-dde7-43e8-a87d-db99",
"503c-0c22-4f94-ad3a-7575",
"c40d-bab5-4228-a968-b055",
"82f5-d8ec-4f4f-99c4-b4f0",
"cd75-47e0-4d2c-9682-d803",
"9bc3-bd98-4bc7-82bf-02c6",
"23fc-9b00-4cc8-91c1-b13e",
"f3fa-494e-411e-94a0-8388",
"2a4a-f85c-4fc1-ba1e-3540",
"0180-f82c-470b-b368-5048",
"70e9-def9-44a6-b4e4-f945",
"9015-c046-4cbb-b0f9-bd53",
"10b2-9473-4227-aef5-8545",
"974b-7be1-4b20-b44c-78b8",
"3999-6762-472b-8dc7-09c0",
"8208-4241-4620-bf3b-eb18",
"8d7f-def9-45fa-8169-3e17",
"5bce-744c-473b-98d3-e149",
"1eb1-8ee0-46ae-82c9-28c5",
"7220-bbd8-40a4-baf4-78c9",
"c1e2-6276-4ad0-ac58-ed7c",
"0c68-9fd5-46cd-92a3-9e95",
"2fa4-2473-4b38-b9dd-a670",
"6715-0569-4699-b341-ecb6",
"9b64-9725-461e-9659-623f",
"b6ff-d269-4236-a7be-56f4",
"fbcb-6eb2-4f2b-9a91-edac",
"2f7e-eb75-4f01-9e52-c374",
"58ec-70b7-4500-9a85-2b04",
"13f2-7a28-4711-9365-6a10",
"4694-a87a-4ba9-942f-73fb",
"2ac8-1694-4a06-ac5e-50f8",
"cad5-5451-4601-aba3-b011",
"7cc6-eb1a-49e2-a84c-3132",
"721b-83ab-450f-aef0-b2c6",
"8b44-54da-4500-a269-0f21",
"9501-d589-4d5b-ab22-e9b3",
"3031-1aa7-4e12-82cf-bd53",
"d746-7cb4-48ad-bcb6-09fe",
"fd0f-2658-4ce4-b08d-c7cf",
"0923-b0e4-412b-9f99-6003",
"96b4-e619-4297-ba14-5066",
"0c50-2030-4c09-9690-bdcf",
"145a-271f-4d64-a8e5-a42f",
"28d1-1d87-4d42-bfbb-84f6",
"4a4d-8968-4511-b513-1d12",
"4906-5a4b-41d3-bf72-006c",
"0b2b-5eef-4746-b629-375e",
"da50-6b51-4af0-a708-3e4e",
"db15-61d2-4455-859a-c5e4",
"3ea7-1472-4c88-a9c5-d03f",
"1c78-a145-473a-a7a6-7e2d",
"2ed6-152f-461e-901b-d470",
"2a5a-85a9-4202-86f1-317b",
"8d4a-3a3f-4f47-9fcd-cd0c",
"6b2c-b2e2-4cae-b11d-366e",
"4561-4014-4435-9b15-1609",
"6987-f86b-4724-88e2-bac7",
"e0a8-d8f8-4de6-90c1-cf8d",
"c881-bcd1-4f76-9c34-b635",
"fc90-5167-4e69-99b1-0dcb",
"161b-6b4e-4270-aaa9-3c6e",
"228d-f3fc-4cec-ab04-79b5",
"4332-bfa7-455d-bb2b-2f49",
"d7b5-b020-4f31-b22c-9211",
"b65e-f1f1-46bd-a7af-e327",
"644d-7da8-4b1d-a12f-6ad5",
"8b51-7f07-4ac9-800f-088c",
"2d31-1e7b-4716-9482-c3c9",
"8823-5409-464d-9c1d-7fee",
"ba0c-7b78-43cb-beb5-9cb4",
"e104-0a74-4a0b-98b9-51dd",
"b6cb-368b-422d-ae75-fcf9",
"c5f2-a686-475e-a4de-6ca3",
"6ba0-7d5d-43d3-b478-e5c4",
"cbb1-cd3e-4dae-918e-8c8b",
"39bb-7695-4871-a864-57f0",
"9252-ddd6-4e49-8c5e-f715",
"26e4-e15f-48b8-932c-09a5",
"8663-8649-4a25-a67d-b9c9",
"bfa2-aac1-4c51-a55f-46c0",
"eb7e-1bd6-4717-a747-1440",
"ed19-4e6f-457b-8e8c-d539",
"e360-439c-4be9-be39-0862",
"1f3a-8842-423b-a3fd-cc8b",
"dce4-e823-4479-a9aa-29ab",
"a1fb-4ab7-4af2-9463-1428",
"079f-b316-4ef6-ae0e-4bc7",
"6df5-06bc-4d1f-8fb0-a675",
"ca7d-81d8-4176-abf6-5f1b",
"a0c9-57b9-498a-8180-63a1",
"25da-1c56-47da-957b-0633",
"95fe-c8e0-4b12-95bd-c88a",
"5c83-990b-4814-9150-d6bf",
"4e03-a75d-4071-8122-d8b4",
"651f-d2dd-4b12-98d5-04f9",
"bd24-782b-45e7-9518-06af",
"2a21-51ba-4b6a-a031-e69d",
"995f-7d80-42dd-aa0d-9099",
"e4db-1d42-4ffb-932c-56ba",
"db33-afb9-4f0d-9c7b-bb80",
"c05c-4f4d-4eb8-842b-73cf",
"520b-94fe-455c-a3cd-317e",
"3be0-ddc9-4750-9aee-8080",
"ad8d-7676-4968-9965-0cc4",
"3372-e752-42fc-a5c4-66ff",
"ce32-1825-404a-a5ae-6a29",
"bfe2-17cd-42a1-9f48-9e7d",
"f3f1-d5e5-48e7-82b4-da7d",
"ffcd-07ed-4d87-b3fc-6015",
"60b8-bab4-41ca-b3de-9ebd",
"e2a3-ac78-4b01-9ed2-f0b7",
"8b6c-8c6d-4aea-9cab-84cc",
"9406-dc2b-411c-b7d1-cb3a",
"ea23-0520-41b1-be39-65f2",
"4c29-d501-4559-a59c-4280",
"eb0e-fdc8-4a45-bd8d-87f8",
"1e5e-06d6-4a6f-a13a-f4e4",
"ca6c-3593-4935-9639-90ac",
"bdbe-4aae-4f15-8688-1439",
"c9c0-963d-4fd4-b31e-2627",
"8e63-43c7-4d34-83db-121b",
"156d-cf32-4285-a9e7-4f74",
"7cdf-f9c8-4848-9dbe-b5a8",
"63a9-02a9-4ac0-b9f6-b1f0",
"5c77-f793-4d87-98db-f019",
"e073-e0bf-41f6-87fc-0b8c",
"c52b-8f28-4064-b8a4-dfda",
"fb43-3998-488e-838d-8ccf",
"4510-2938-4a6b-afba-6853",

            };

            string key = keys[keyIndex];

            return keys[keyIndex].ToUpper();
        }

        private void mtbxSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (mtbxSerial.Text == "    -    -    -    -")
            {
                mtbxSerial.Select(0, 0);
            }

        }
        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Icon = SystemIcons.Application;
            currentDateTime = DateTime.Now;

            
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in networkInterfaces)
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    physicalAddressList.Add(item.GetPhysicalAddress());
                }
            }

            try { currentDateTime = OnlineTime.GetOnlineTime(); } catch { }

            //yeni yıl versiyonuna geçerken daha önce lisanslandı mı kontrolü için kullandığımız formülü güncelle!!!!!!!!
            //yıl kontrolü hem burada hem FormMainde yapılmakta

            if (currentDateTime >= new DateTime(2025, 2, 15))
            {
                MessageBox.Show("Mkys Tdms Uyumu programının bu sürümünün süresi sona erdi. Lütfen yeni versiyon yükleyiniz.");
                LicenceSetting.Default.Licenced = false;
                LicenceSetting.Default.Save();
                this.Close();
            }

            string keyCode = "";
            
            if (physicalAddressList.Count > 0)
            {
                //mac adresindeki sayıları alıyoruz
                foreach (char item in physicalAddressList[0].ToString())
                {
                    if (keyCode.Length < 12)
                    {
                        int number = Convert.ToInt32(item);
                        keyCode += number.ToString();
                    }
                    else
                    {
                        break;
                    }
                }
                //gelen sayının uzunluğunu 6 haneye ayarlıyoruz
                if (keyCode.Length >= 12)
                {
                    keyCode = keyCode.Substring(0, 12);
                }
                else
                {
                    for (int i = 0; i < 12 - keyCode.Length; i++)
                    {
                        keyCode += i.ToString();
                    }
                }


                //sayıların sıralamaını karıştırıyoruz.
                keyCode =
                    keyCode[1].ToString() +
                    keyCode[8].ToString() +
                    keyCode[4].ToString() +
                    "-" +
                    keyCode[10].ToString() +
                    keyCode[3].ToString() +
                    keyCode[7].ToString();
            }
            else
            {
                //eğer fiziki adres boş ise bunu verecek
                //yeni yılda değiştirilsin
                keyCode = "247-826";
            }
            keyIndex = Convert.ToInt32(keyCode.Substring(0, 3)+keyCode.Substring(4,3)) % 500;
            lblActivationCode.Text ="AKTİVASYON KODUNUZ: " + keyCode;
            

        }

        public bool licencedBefore()
        {
            bool licenced = false;
            if (!File.Exists(path))
            {
                return false;
            }
            string cryptedText = File.ReadAllText(path);
            if (cryptedText.Length!=48)
            {
                LicenceSetting.Default.Licenced = false;
                LicenceSetting.Default.Save();
                return false;
                
            }

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in networkInterfaces)
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    physicalAddressList.Add(item.GetPhysicalAddress());
                }
            }

            try {
                
                string macAddress = Crypt.Decrypt(cryptedText);
                foreach (PhysicalAddress address in physicalAddressList)
                {
                    if (address.ToString() == macAddress)
                    {
                        licenced = true;
                        LicenceSetting.Default.Licenced = true;
                        LicenceSetting.Default.Save();
                        Settings.Default.dontShowWelcome = true;
                        Settings.Default.Save();
                    }
                }

            } catch { }
            


            return licenced;
        }
    }
}
