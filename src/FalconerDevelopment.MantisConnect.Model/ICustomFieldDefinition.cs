using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface ICustomFieldDefinition
    {
        AccessLevel AccessLevelR{ get; }

        AccessLevel AccessLevelRW{ get; }

        bool? Advanced { get; }

        String DefaultValue{ get; }

        bool? DisplayClose{ get; }

        bool? DisplayReport{ get; }

        bool? DisplayResolve{ get; }

        bool? DisplayUpdate{ get; }

        IMCAttribute Field{ get; }

        long LengthMax{ get; }

        long LengthMin{ get; }

        String PossibleValues{ get; }

        bool? RequireClosed{ get; }

        bool? RequireReport{ get; }

        bool? RequireResolved{ get; }

        bool? RequireUpdate{ get; }

        CustomFieldType Type{ get; }

        String ValidRegexp{ get; }
    }
}