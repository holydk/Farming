using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class CowViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Идентификатор породы")]
        public int? BreedId
        {
            get => Get(() => BreedId);
            set => Set(() => BreedId, value);
        }

        [Description("Идентификатор линии")]
        public int? LineId
        {
            get => Get(() => LineId);
            set => Set(() => LineId, value);
        }

        [Description("Идентификатор семейства")]
        public int? FamilyId
        {
            get => Get(() => FamilyId);
            set => Set(() => FamilyId, value);
        }

        [Description("Идентификатор категории")]
        public int? CategoryId
        {
            get => Get(() => CategoryId);
            set => Set(() => CategoryId, value);
        }

        [Description("Идентификатор группы крови")]
        public int? BloodTypeId
        {
            get => Get(() => BloodTypeId);
            set => Set(() => BloodTypeId, value);
        }

        [Description("Идентификатор по материнской линии")]
        public int? MotherId
        {
            get => Get(() => MotherId);
            set => Set(() => MotherId, value);
        }

        [Description("Идентификатор по отцовской линии")]
        public int? FatherId
        {
            get => Get(() => FatherId);
            set => Set(() => FatherId, value);
        }

        [Description("Породность")]
        public double? Porodnost
        {
            get => Get(() => Porodnost);
            set => Set(() => Porodnost, value);
        }

        [Description("Имя")]
        public string Name
        {
            get => Get(() => Name);
            set => Set(() => Name, value);
        }

        [Description("Дата рождения")]
        public DateTime? BDay
        {
            get => Get(() => BDay);
            set => Set(() => BDay, value);
        }

        [Description("Место рождения")]
        public string BPlace
        {
            get => Get(() => BPlace);
            set => Set(() => BPlace, value);
        }

        [Description("Вес")]
        public double? Weight
        {
            get => Get(() => Weight);
            set => Set(() => Weight, value);
        }

        [Description("Возраст")]
        public int? Age
        {
            get => Get(() => Age);
            set => Set(() => Age, value);
        }

        [Description("В стаде")]
        public bool? IsInHerd
        {
            get => Get(() => IsInHerd);
            set => Set(() => IsInHerd, value);
        }

        public virtual BullViewModel Father
        {
            get => Get(() => Father);
            set
            {
                Set(() => Father, value);

                if (Father != null)
                    FatherId = Father.Id;
            }
        }

        public virtual BloodTypeViewModel BloodType
        {
            get => Get(() => BloodType);
            set
            {
                Set(() => BloodType, value);

                if (BloodType != null)
                    BloodTypeId = BloodType.Id;
            }
        }

        public virtual CategoryViewModel Category
        {
            get => Get(() => Category);
            set
            {
                Set(() => Category, value);

                if (Category != null)
                    CategoryId = Category.Id;
            }
        }

        public virtual CowViewModel Mother
        {
            get => Get(() => Mother);
            set
            {
                Set(() => Mother, value);

                if (Mother != null)
                    MotherId = Mother.Id;
            }
        }

        public virtual LineViewModel Line
        {
            get => Get(() => Line);
            set
            {
                Set(() => Line, value);

                if (Line != null)
                    LineId = Line.Id;
            }
        }

        public virtual BreedViewModel Breed
        {
            get => Get(() => Breed);
            set
            {
                Set(() => Breed, value);

                if (Breed != null)
                    BreedId = Breed.Id;
            }
        }

        public virtual FamilyViewModel Family
        {
            get => Get(() => Family);
            set
            {
                Set(() => Family, value);

                if (Family != null)
                    FamilyId = Family.Id;
            }
        }

        public virtual ICollection<CowViewModel> Childrens
        {
            get => Get(() => Childrens);
            set => Set(() => Childrens, value);
        }

        public virtual ICollection<PriplodViewModel> Priplods
        {
            get => Get(() => Priplods);
            set => Set(() => Priplods, value);
        }

        public virtual ICollection<ProductivityViewModel> Productivities
        {
            get => Get(() => Productivities);
            set => Set(() => Productivities, value);
        }

        public virtual ICollection<RetirementViewModel> Retirements
        {
            get => Get(() => Retirements);
            set => Set(() => Retirements, value);
        }

        public virtual ICollection<ReproductionViewModel> Reproductions
        {
            get => Get(() => Reproductions);
            set => Set(() => Reproductions, value);
        }

        public CowViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => Name, () => !string.IsNullOrWhiteSpace(Name) && Name.Length < 50, "Имя не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Porodnost, () => Porodnost.HasValue && Porodnost > 0, "Поле должно быть больше 0.");
            AddRule(() => Weight, () => Weight.HasValue && Weight > 0, "Поле должно быть больше 0.");
            AddRule(() => Age, () => Age.HasValue && Age > 0, "Поле должно быть больше 0.");
            AddRule(() => IsInHerd, () => IsInHerd.HasValue, "Поле не должно быть неопределенным.");
            AddRule(() => BPlace, () => !string.IsNullOrWhiteSpace(BPlace) && BPlace.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => BDay, () => BDay.HasValue, "Поле не должно быть пустым.");
            //AddRule(() => Father, () => Father != null && FatherId == Father.Id && Father.Valid(), "Поле не должно быть пустым.");
            AddRule(() => BloodType, () => BloodType != null && BloodTypeId == BloodType.Id && BloodType.Valid(), "Поле не должно быть пустым.");
            AddRule(() => Category, () => Category != null && CategoryId == Category.Id && Category.Valid(), "Поле не должно быть пустым.");
            //AddRule(() => Mother, () => Mother != null && MotherId == Mother.Id && Mother.Valid() && Mother.Id != Id, "Поле не должно быть пустым и идентификатор матери не должен совпадать с текущей коровой.");
            AddRule(() => Line, () => Line != null && LineId == Line.Id && Line.Valid(), "Поле не должно быть пустым.");
            AddRule(() => Breed, () => Breed != null && BreedId == Breed.Id && Breed.Valid(), "Поле не должно быть пустым.");
            AddRule(() => Family, () => Family != null && FamilyId == Family.Id && Family.Valid(), "Поле не должно быть пустым.");

            Childrens = new ObservableCollection<CowViewModel>();
            Priplods = new ObservableCollection<PriplodViewModel>();
            Productivities = new ObservableCollection<ProductivityViewModel>();
            Retirements = new ObservableCollection<RetirementViewModel>();
            Reproductions = new ObservableCollection<ReproductionViewModel>();
        }
    }
}
