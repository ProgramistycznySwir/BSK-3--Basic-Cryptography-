using Main;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Console.WriteLine("Testing RailFence.");
string Zad1_0_word = "CRYPTOGRAPHY";
string Zad1_1_word = "ABCDEFGHIJKL";

string Zad1_1_word_encrypted = new RailFence(railCount: 3).Encrypt(Zad1_1_word).CollectString();
string Zad1_1_word_encrypted_decrypted = new RailFence(railCount: 3).Decrypt(Zad1_1_word_encrypted).CollectString();

