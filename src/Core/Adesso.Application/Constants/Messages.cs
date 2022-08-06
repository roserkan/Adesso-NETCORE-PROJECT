
namespace Adesso.Application.Constants;

public static class Messages
{
    // Categories

    public static string CategoryCreated = "Kategori başarıyla eklenmiştir.";
    public static string CategoryUpdated = "Kategori başarıyla güncellenmiştir.";
    public static string CategoryDeleted = "Kategori başarıyla silinmiştir.";
    public static string CategoryIdNotFound = "Böyle bir kategori bulunmamaktadır.";

    public static string CategoryNameNotNull = "Kategori adı boş olamaz!";
    public static string CategoryNameMinLen = "Kategori adı en az 2 karakter uzunluğunda olmalı!";
    public static string CategoryNameMaxLen = "Kategori adı en fazla 16 karakter uzunluğunda olmalı!";
    public static string CategoryIdNotNull = "Kategori adı boş olamaz!";
    public static string CategoryNameAlreadyExist = "Kategori adı zaten var!";

    // Products

    public static string ProductCreated = "Ürün başarıyla eklenmiştir.";
    public static string ProductUpdated = "Ürün başarıyla güncellenmiştir.";
    public static string ProductDeleted = "Ürün başarıyla silinmiştir.";

    public static string ProductNotFound = "Böyle bir ürün bulunmamaktadır.";

    public static string ProductNameNotNull = "Ürün adı boş olamaz!";
    public static string ProductNameMinLen = "Ürün adı en az 2 karakter uzunluğunda olmalı!";
    public static string ProductNameMaxLen = "Kategori adı en fazla 100 karakter uzunluğunda olmalı!";

    public static string ProductPriceNotNull = "Ürün fiyatı boş olamaz!";
    public static string ProductPriceMin = "Ürün fiyatı 0 değerinden büyük olmalı!";
    public static string ProductCategoryIdNotNull = "Ürün kategorisi boş olamaz!";
    public static string ProductIdNotNull = "Ürün boş olamaz!";
    public static string ProductIdNotFound = "Böyle bir ürün bulunmamaktadır!";
    public static string ProductImagePathMaxLen = "Görsel resmi çok büyük!";
    public static string ProductNameAlreadyExist = "Ürün zaten var!";




    // User
    public static string UserCreated = "Kullanıcı başarıyla eklenmiştir.";
    public static string UserUpdated = "Kullanıcı başarıyla güncellenmişti.";
    public static string UserDeleted = "Kullanıcı başarıyla silinmiştir.";

    public static string UserNotFound = "Kullanıcı bulunamadı!";
    public static string UserEmailAddressNotAvailable = "E-posta daha önce kullanılmış!";
    public static string UserEmailAddressNotValid = "E-posta geçersiz!";
    public static string UserPasswordMinLen = "Şifre en az 6 karakter uzunluğunda olmalı!";
    public static string UserPasswordMaxLen = "Şifre en fazla 16 karakter uzunluğunda olmalı!";
    public static string PasswordWrongError = "Şifre hatalı!";


    // Money Point
    public static string MoneyPointNotFound = "Para puan bulunamadı!";



    // Order
    public static string OrderNotFound = "Sipariş bulunamadı!";

    // Order
    public static string OrderItemNotFound = "Sipariş ürünü bulunamadı!";


    // UserDetail
    public static string UserDetailNotFound = "Kullanıcı bulunamadı!";


}