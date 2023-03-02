import isNumber from "lodash/isNumber";

export const formatNumber = (num, precision) => {
  if (!isNumber(num)) {
    return "";
  }
  const _num = isNumber(precision) ? num.toFixed(precision) : num;
  return _num
    .toString()
    .replace(/^[+-]?\d+/, (int) => int.replace(/(\d)(?=(\d{3})+$)/g, "$1,"));
};

export const removeNumberFormat = (string) => {
  return string && string.length > 0 ? string.replaceAll(",", "") : 0;
};

export const validateInputNumberType = (num) => {
  if (!num || isNaN(num)) return 0;
  return num.toString().replace(/\D/g, "");
};
