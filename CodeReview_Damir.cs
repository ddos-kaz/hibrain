Code Review - Досжан Дамир

Для разрабора ошибок надо расмотреть методы по отдельности. 
*****************************************************************************************************************
Для начало давайте расмотрим метод Parse.  У данного метода присуствует синтакстические
ошибки:
1) public int Parse(object o)  -> здесь нужно написать тип с большой буквы (подразумеваю 
   что используем класс java.lang)
2) o.ToString() -> для использование метода предстовления объекта в виде строки нужно
   написать toString().
   
Далее я расмотрю общие проблемы метода Parse:
1) У целочисленного типа int нету методов так как int не является объяктом Java.
   int является примитивный типом и для данной ситуации нужно использовать класс Integer 
   с методом parseInt(). 
2) В условиях метода указано что если парсирование будет прервано нужно возращать 0.
   В текущей реализации не было учтено ситуации когда обьект не является типом int. 
   При входных данных когда объект o является null, компилятор выдаст ошибку NullPointerException.
   При входных данных когда объект не является null и int, компилятор выдаст ошибку NumberFormatException.
   Для решения данной проблемы можно использовать оператор instanceOf:
    public int Parse(Object o)
	{
		return o instanceof Integer ? Integer.parseInt(o.toString()) : 0;
	}
	Или же использовать использовать исключение (try-catch), который будет охватывать больше
	вариантов входных данных ( Например при использование целочисленных значений в String)
	public int Parse(Object o)
	{
		int result;
		try {
			result = Integer.parseInt(o.toString());
		} catch (NumberFormatException|NullPointerException e){
			result = 0; 
		}
		return result;
	}

*******************************************************************************************************************

Давайте расмотрим метод Concat который должен объединять массив сторк в единственную строку. В данном методе
также присуствует синтакстические ошибки такие как:
1) Объевление класса String с маленькой буквы (string)
2) Использование метода Array.length с большой буквы Length

Далее я расмотрю общие проблемы метода Concat:
1) Для использования текущей версии метода нужно провести инициализация переменной String. 
	public String Concat(String[] lines) {
		String result = new String();
		for (int i = 0; i < lines.length; i++)
		{
			result += lines[i];
		}
		return result;
	}
2) Использование +, += оператора объединения строк является не эффиктивным методом. Тип String является неизменяемым (immutable)
   и при каждой конкатенации создается новый объект что лишний раз расходует память. Также если в передоваемым
   массиве будет null, оператор +,+= напрямую добавит к финальной строке значение null. (Например, ["Hello", null, "World"]
   будет -> "HellonullWorld"). Для конкатенации нескольки строк в один объект (не создавая новый объект), нужно испольвать 
   StringBuffer/StringBuilder которые являются mutable. Так как пример не предусматривает многопоточность для ускорения 
   конкатенации будем использовать StringBuilder.
   public String Concat(String[] lines) {
		StringBuilder result = new StringBuilder();
		for (int i = 0; i < lines.length; i++)
		{
				result.append(lines[i]!=null?lines[i]:"");
		}
		return result.toString();
	}
	
*******************************************************************************************************************

Финальный вариант кода:

public class CodeReview {
	public int Parse(Object o) {
		// return o instanceof Integer ? Integer.parseInt(o.toString()) : 0;
		int result;
		try {
			result = Integer.parseInt(o.toString());
		} catch (NumberFormatException | NullPointerException e) {
			result = 0;
		}
		return result;
	}

	public String Concat(String[] lines) {
		StringBuilder result = new StringBuilder();
		for (int i = 0; i < lines.length; i++)
		{
				result.append(lines[i]!=null?lines[i]:"");
		}
		return result.toString();
	}
}
