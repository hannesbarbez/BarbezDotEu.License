<a name='assembly'></a>
# BarbezDotEu.License.Verification

## Contents

- [KeyVerificator](#T-BarbezDotEu-License-Verification-KeyVerificator 'BarbezDotEu.License.Verification.KeyVerificator')
  - [ValidKey()](#M-BarbezDotEu-License-Verification-KeyVerificator-ValidKey-System-String,System-String,System-String,System-String,System-String- 'BarbezDotEu.License.Verification.KeyVerificator.ValidKey(System.String,System.String,System.String,System.String,System.String)')
  - [ValidateSegment(segment)](#M-BarbezDotEu-License-Verification-KeyVerificator-ValidateSegment-System-String- 'BarbezDotEu.License.Verification.KeyVerificator.ValidateSegment(System.String)')
  - [VerifyKey()](#M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String,System-String,System-String,System-String,System-String- 'BarbezDotEu.License.Verification.KeyVerificator.VerifyKey(System.String,System.String,System.String,System.String,System.String)')

<a name='T-BarbezDotEu-License-Verification-KeyVerificator'></a>
## KeyVerificator `type`

##### Namespace

BarbezDotEu.License.Verification

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-ValidKey-System-String,System-String,System-String,System-String,System-String-'></a>
### ValidKey() `method`

##### Summary

Checks if inputted key is valid

##### Returns

True if valid, false if invalid.

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-ValidateSegment-System-String-'></a>
### ValidateSegment(segment) `method`

##### Summary

Checks if a segment valid.

##### Returns

True if valid, false if invalid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| segment | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The segment to interpret. |

<a name='M-BarbezDotEu-License-Verification-KeyVerificator-VerifyKey-System-String,System-String,System-String,System-String,System-String-'></a>
### VerifyKey() `method`

##### Summary

Checks if a key is valid as well as all of its segments.

##### Returns

True if the key is valid; false otherwise.

##### Parameters

This method has no parameters.
