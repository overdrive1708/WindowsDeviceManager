using CommandLine;
using CommandLine.Text;

namespace WindowsDeviceManagerAgent
{
    public class LocalizableSentenceBuilder : SentenceBuilder
    {
        public override Func<string> RequiredWord => () => Resources.Strings.SentenceRequiredWord;

        public override Func<string> OptionGroupWord => () => Resources.Strings.SentenceOptionGroupWord;

        public override Func<string> ErrorsHeadingText => () => Resources.Strings.SentenceErrorsHeadingText;

        public override Func<string> UsageHeadingText => () => Resources.Strings.SentenceUsageHeadingText;

        public override Func<bool, string> HelpCommandText => isOption => isOption
                                                                               ? Resources.Strings.SentenceHelpCommandTextOption
                                                                               : Resources.Strings.SentenceHelpCommandTextVerb;

        public override Func<bool, string> VersionCommandText => _ => Resources.Strings.SentenceVersionCommandText;

        public override Func<Error, string> FormatError
        {
            get
            {
                return error =>
                {
                    switch (error.Tag)
                    {
                        case ErrorType.BadFormatTokenError:
                            return string.Format(Resources.Strings.SentenceBadFormatTokenError, ((BadFormatTokenError)error).Token);

                        case ErrorType.MissingValueOptionError:
                            return string.Format(Resources.Strings.SentenceMissingValueOptionError, ((MissingValueOptionError)error).NameInfo.NameText);

                        case ErrorType.UnknownOptionError:
                            return string.Format(Resources.Strings.SentenceUnknownOptionError, ((UnknownOptionError)error).Token);

                        case ErrorType.MissingRequiredOptionError:
                            MissingRequiredOptionError errMisssing = (MissingRequiredOptionError)error;
                            return errMisssing.NameInfo.Equals(NameInfo.EmptyName)
                                       ? Resources.Strings.SentenceMissingRequiredOptionErrorValue
                                       : string.Format(Resources.Strings.SentenceMissingRequiredOptionErrorOption, errMisssing.NameInfo.NameText);

                        case ErrorType.BadFormatConversionError:
                            BadFormatConversionError badFormat = (BadFormatConversionError)error;
                            return badFormat.NameInfo.Equals(NameInfo.EmptyName)
                                       ? Resources.Strings.SentenceBadFormatConversionErrorValue
                                       : string.Format(Resources.Strings.SentenceBadFormatConversionErrorOption, badFormat.NameInfo.NameText);

                        case ErrorType.SequenceOutOfRangeError:
                            SequenceOutOfRangeError seqOutRange = (SequenceOutOfRangeError)error;
                            return seqOutRange.NameInfo.Equals(NameInfo.EmptyName)
                                       ? Resources.Strings.SentenceSequenceOutOfRangeErrorValue
                                       : string.Format(Resources.Strings.SentenceSequenceOutOfRangeErrorOption, seqOutRange.NameInfo.NameText);

                        case ErrorType.BadVerbSelectedError:
                            return string.Format(Resources.Strings.SentenceBadVerbSelectedError, ((BadVerbSelectedError)error).Token);

                        case ErrorType.NoVerbSelectedError:
                            return Resources.Strings.SentenceNoVerbSelectedError;

                        case ErrorType.RepeatedOptionError:
                            return string.Format(Resources.Strings.SentenceRepeatedOptionError, ((RepeatedOptionError)error).NameInfo.NameText);

                        case ErrorType.SetValueExceptionError:
                            SetValueExceptionError setValueError = (SetValueExceptionError)error;
                            return string.Format(Resources.Strings.SentenceSetValueExceptionError, setValueError.NameInfo.NameText, setValueError.Exception.Message);

                        case ErrorType.MissingGroupOptionError:
                            MissingGroupOptionError missingGroupOptionError = (MissingGroupOptionError)error;
                            return string.Format(Resources.Strings.MissingGroupOptionError, missingGroupOptionError.Group, missingGroupOptionError.Names.Select(n => n.NameText));

                        case ErrorType.GroupOptionAmbiguityError:
                            GroupOptionAmbiguityError groupOptionAmbiguityError = (GroupOptionAmbiguityError)error;
                            return string.Format(Resources.Strings.GroupOptionAmbiguityError, groupOptionAmbiguityError.Option.NameText);

                        case ErrorType.MultipleDefaultVerbsError:
                            return MultipleDefaultVerbsError.ErrorMessage;

                        default:
                            throw new InvalidOperationException();
                    }
                };
            }
        }

        public override Func<IEnumerable<MutuallyExclusiveSetError>, string> FormatMutuallyExclusiveSetErrors
        {
            get
            {
                return errors =>
                {
                    var bySet = from error in errors
                                group error by error.SetName into g
                                select new { SetName = g.Key, Errors = g.ToList() };

                    string[] msgs = bySet.Select(
                        set =>
                        {
                            string names = string.Join(
                                string.Empty,
                                (from e in set.Errors select string.Format("'{0}', ", e.NameInfo.NameText)).ToArray());
                            int namesCount = set.Errors.Count;

                            string incompat = string.Join(
                                string.Empty,
                                (from x in
                                    (from s in bySet where !s.SetName.Equals(set.SetName) from e in s.Errors select e)
                                    .Distinct()
                                select string.Format("'{0}', ", x.NameInfo.NameText)).ToArray());

                            return string.Format(Resources.Strings.SentenceMutuallyExclusiveSetErrors, names[..^2], incompat[..^2]);
                        }).ToArray();
                    return string.Join(Environment.NewLine, msgs);
                };
            }
        }
    }
}
