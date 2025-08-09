using System.Linq;

namespace WinMorph32 {
    public static class ExeCxtUtilityMethods {

        #region XElement-Serialisation-Logic

        #region Backtickery

        /*
         *
         * WinMorph-DocScript custom XElement serialisation:
         *  (") isn't allowed in DSString values, so use (``) instead. Eg...
         *  <Person Name=``Ben`` Value=``Some (`)(`) chars here.`` />
         * 
         */

        /// <summary>Uses `` instead of ", for attrs; eg name=``BEN``.</summary>
        public static System.String SerializeXElement_WithDoubleBackticks(System.Xml.Linq.XElement _XElementInput) {

            if (_XElementInput == null) { throw new System.ArgumentNullException("_XElementInput"); }

            // Clone the input to avoid mutating the original
            System.Xml.Linq.XElement _XElementClone = new System.Xml.Linq.XElement(_XElementInput);

            // Replace any existing instances of `` in attribute values, with (`)(`)
            foreach (System.Xml.Linq.XElement _DescendantOrSelf in _XElementClone.DescendantsAndSelf()) {
                foreach (System.Xml.Linq.XAttribute _Attribute in _DescendantOrSelf.Attributes()) {
                    if (_Attribute.Value.Contains("``")) {
                        _Attribute.Value = _Attribute.Value.Replace("``", "(`)(`)");
                    }
                }
            }

            // Serialize as string (unformatted, default quote-delimited)
            System.String _SerialisedXml = _XElementClone.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);

            // Replace attribute-encapsulation-chars;eg name="value" → name=``value``
            System.String _DoubleBacktickXml = System.Text.RegularExpressions.Regex.Replace(
                _SerialisedXml,
                "(\\s+[^\\s=<>\\'\\\"]+)=\"([^\"]*)\"",
                _Match => _Match.Groups[1].Value + "=``" + _Match.Groups[2].Value + "``"
            );

            return _DoubleBacktickXml;
        }

        /// <summary>Takes in an XElement which uses `` instead of ", for attrs; eg name=``BEN``.</summary>
        public static System.Xml.Linq.XElement DeserializeXElement_FromDoubleBackticks(System.String _SerializedXElementWithDoubleBackticks) {

            if (_SerializedXElementWithDoubleBackticks == null) { throw new System.ArgumentNullException("_SerializedXElementWithDoubleBackticks"); }

            // Convert attribute delimiters from ``value`` → "value"
            // and restore any "(`)(`)" placeholders back to "``" inside attribute values.

            System.String _StandardQuoteXml = System.Text.RegularExpressions.Regex.Replace(
                _SerializedXElementWithDoubleBackticks,
                "(\\s+[^\\s=<>\\'\\\"]+)=``((?:(?!``).)*)``",
                _Match => {
                    System.String _AttrName = _Match.Groups[1].Value;
                    System.String _AttrValue = _Match.Groups[2].Value;

                    if (!System.String.IsNullOrEmpty(_AttrValue) && _AttrValue.Contains("(`)(`)")) {
                        _AttrValue = _AttrValue.Replace("(`)(`)", "``");
                    }

                    return _AttrName + "=\"" + _AttrValue + "\"";
                }
            );

            System.Xml.Linq.XElement _XElement = System.Xml.Linq.XElement.Parse(_StandardQuoteXml);
            return _XElement;

        }

        #endregion

        /// <summary>Gets an attribute value from an XElement</summary>
        /// <param name="_XElement">XElement containing the attribute</param>
        /// <param name="_AttributeName">Name of the attribute to get</param>
        /// <returns>String value of the attribute, or empty string if not found</returns>
        public static System.String XElement_GetAttribute(System.Xml.Linq.XElement _XElement, System.String _AttributeName) {

            if (_XElement == null) { throw new System.ArgumentNullException("_XElement"); }
            if (System.String.IsNullOrEmpty(_AttributeName)) { throw new System.ArgumentNullException("_AttributeName"); }

            System.Xml.Linq.XAttribute _Attribute = _XElement.Attribute(_AttributeName);
            return (_Attribute != null) ? _Attribute.Value : "";

        }

        /// <summary>Sets an attribute value in an XElement and returns a new XElement with the updated value</summary>
        /// <param name="_XElement">XElement to update</param>
        /// <param name="_AttributeName">Name of the attribute to set</param>
        /// <param name="_NewValue">New value for the attribute</param>
        /// <returns>New XElement with the updated attribute value</returns>
        public static System.Xml.Linq.XElement XElement_SetAttribute(System.Xml.Linq.XElement _XElement, System.String _AttributeName, System.String _NewValue) {

            if (_XElement == null) { throw new System.ArgumentNullException("_XElement"); }
            if (System.String.IsNullOrEmpty(_AttributeName)) { throw new System.ArgumentNullException("_AttributeName"); }
            if (_NewValue == null) { _NewValue = ""; }

            // Create a copy of the original XElement to avoid modifying the input
            System.Xml.Linq.XElement _NewXElement = new System.Xml.Linq.XElement(_XElement);
            _NewXElement.SetAttributeValue(_AttributeName, _NewValue);

            return _NewXElement;

        }

        /// <summary>
        /// Serializes any object to an XElement, with the object's type name as the _XElement name,
        /// and all public _InputObjFields/_InputObjProperties as attributes
        /// </summary>
        /// <typeparam name="TInputObject">Type of object to serialize</typeparam>
        /// <param name="_InputObject">Object instance to serialize</param>
        /// <returns>XElement representation of the object</returns>
        public static System.Xml.Linq.XElement ToXElement<TInputObject>(TInputObject _InputObject) {

            if (_InputObject == null) { throw new System.ArgumentNullException("_InputObject"); }
            System.Xml.Linq.XElement _XElement = new System.Xml.Linq.XElement(typeof(TInputObject).Name);

            // Get all public fields (data-members)
            System.Reflection.FieldInfo[] _InputObjFields = typeof(TInputObject).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            
            foreach (System.Reflection.FieldInfo _Field in _InputObjFields) {
                System.Object _FieldValue = _Field.GetValue(_InputObject);
                System.String _FieldValueString = (_FieldValue != null) ? _FieldValue.ToString() : "";
                _XElement.SetAttributeValue(_Field.Name, _FieldValueString);
            }

            // Get all public _InputObjProperties with getters
            System.Reflection.PropertyInfo[] _InputObjProperties = typeof(TInputObject).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0) // Exclude indexers
                .ToArray()
            ;

            foreach (System.Reflection.PropertyInfo _Property in _InputObjProperties) {
                try {
                    System.Object _PropertyValue = _Property.GetValue(_InputObject, null);
                    string _PropertyValueString = (_PropertyValue != null) ? _PropertyValue.ToString() : "";
                    _XElement.SetAttributeValue(_Property.Name, _PropertyValueString);
                } catch (System.Reflection.TargetParameterCountException) {
                    // Skip _InputObjProperties that require parameters (indexers, etc.)
                    continue;
                }
            }

            return _XElement;
        }

        /// <summary>Deserialises an XElement back to an object of type TOutputObject</summary>
        /// <typeparam name="TOutputObject">Type of object to create</typeparam>
        /// <param name="_XElement">XElement containing the serialized data</param>
        /// <returns>New instance of TOutputObject populated from the XML</returns>
        public static TOutputObject FromXElement<TOutputObject>(System.Xml.Linq.XElement _XElement) where TOutputObject : new() {
            
            if (_XElement == null) { throw new System.ArgumentNullException("_XElement"); }
            TOutputObject _OutputObject = new TOutputObject();

            // Get all public fields (data-members)
            System.Reflection.FieldInfo[] _OutputObjFields = typeof(TOutputObject).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (System.Reflection.FieldInfo _Field in _OutputObjFields) {
                System.Xml.Linq.XAttribute _Attribute = _XElement.Attribute(_Field.Name);
                if (_Attribute != null) {
                    try {
                        System.Object _ConvertedValue = ConvertStringToType(_Attribute.Value, _Field.FieldType);
                        _Field.SetValue(_OutputObject, _ConvertedValue);
                    } catch (System.Exception _Ex) {
                        throw new System.InvalidOperationException(
                            System.String.Format("Failed to convert attribute '{0}' with value '{1}' to type {2}",
                            _Field.Name, _Attribute.Value, _Field.FieldType.Name), _Ex);
                    }
                }
            }

            // Get all public properties with setters
            System.Reflection.PropertyInfo[] _OutputObjProperties = typeof(TOutputObject).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0)
                .ToArray()
            ;

            foreach (System.Reflection.PropertyInfo _Property in _OutputObjProperties) {
                System.Xml.Linq.XAttribute _Attribute = _XElement.Attribute(_Property.Name);
                if (_Attribute != null) {
                    try {
                        System.Object _ConvertedValue = ConvertStringToType(_Attribute.Value, _Property.PropertyType);
                        _Property.SetValue(_OutputObject, _ConvertedValue, null);
                    } catch (System.Exception _Ex) {
                        throw new System.InvalidOperationException(
                            System.String.Format("Failed to convert attribute '{0}' with value '{1}' to type {2}",
                            _Property.Name, _Attribute.Value, _Property.PropertyType.Name), _Ex);
                    }
                }
            }

            return _OutputObject;

        }

        /// <summary>Converts a string value, to an object of the specified type</summary>
        /// <param name="_Value">String value to convert</param>
        /// <param name="_TargetType">Target type to convert to</param>
        /// <returns>Converted value</returns>
        private static System.Object ConvertStringToType(System.String _Value, System.Type _TargetType) {

            // Handle null/empty values
            if (System.String.IsNullOrEmpty(_Value)) {
                if (_TargetType.IsValueType) {
                    // Default value for value types
                    return System.Activator.CreateInstance(_TargetType);
                }
                // `null` for reference types
                return null;
            }

            // Handle nullable types
            if (_TargetType.IsGenericType && _TargetType.GetGenericTypeDefinition() == typeof(System.Nullable<>)) {
                System.Type _UnderlyingType = System.Nullable.GetUnderlyingType(_TargetType);
                System.Object _ConvertedValue = ConvertStringToType(_Value, _UnderlyingType);
                return _ConvertedValue;
            }

            // Handle string type directly
            if (_TargetType == typeof(System.String)) {
                return _Value;
            }

            // Handle enums
            if (_TargetType.IsEnum) {
                return System.Enum.Parse(_TargetType, _Value, true);
            }

            // Use Convert.ChangeType for most other types
            try {
                return System.Convert.ChangeType(_Value, _TargetType);
            } catch (System.Exception) {
                // If Convert.ChangeType fails, try TypeConverter
                System.ComponentModel.TypeConverter _Converter = System.ComponentModel.TypeDescriptor.GetConverter(_TargetType);
                if (_Converter != null && _Converter.CanConvertFrom(typeof(System.String))) {
                    return _Converter.ConvertFromString(_Value);
                }
                throw;
            }

        }

        #endregion

    }
}