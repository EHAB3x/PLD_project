
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF              =  0, // (EOF)
        SYMBOL_ERROR            =  1, // (Error)
        SYMBOL_WHITESPACE       =  2, // Whitespace
        SYMBOL_MINUS            =  3, // '-'
        SYMBOL_MINUSMINUS       =  4, // '--'
        SYMBOL_EXCLAMEQ         =  5, // '!='
        SYMBOL_QUOTE            =  6, // '"'
        SYMBOL_PERCENT          =  7, // '%'
        SYMBOL_LPAREN           =  8, // '('
        SYMBOL_RPAREN           =  9, // ')'
        SYMBOL_TIMES            = 10, // '*'
        SYMBOL_TIMESTIMES       = 11, // '**'
        SYMBOL_DIV              = 12, // '/'
        SYMBOL_COLON            = 13, // ':'
        SYMBOL_SEMI             = 14, // ';'
        SYMBOL_LBRACKET         = 15, // '['
        SYMBOL_BACKSLASH        = 16, // '\'
        SYMBOL_RBRACKET         = 17, // ']'
        SYMBOL_CARET            = 18, // '^'
        SYMBOL_LBRACE           = 19, // '{'
        SYMBOL_RBRACE           = 20, // '}'
        SYMBOL_PLUS             = 21, // '+'
        SYMBOL_PLUSPLUS         = 22, // '++'
        SYMBOL_LT               = 23, // '<'
        SYMBOL_EQ               = 24, // '='
        SYMBOL_EQEQ             = 25, // '=='
        SYMBOL_GT               = 26, // '>'
        SYMBOL_CASE             = 27, // Case
        SYMBOL_DEFAULT          = 28, // Default
        SYMBOL_DIGIT            = 29, // Digit
        SYMBOL_DO               = 30, // do
        SYMBOL_DOUBLE           = 31, // double
        SYMBOL_ELSE             = 32, // else
        SYMBOL_END              = 33, // End
        SYMBOL_FALSE            = 34, // false
        SYMBOL_FLOAT            = 35, // float
        SYMBOL_FOR              = 36, // For
        SYMBOL_ID               = 37, // Id
        SYMBOL_IF               = 38, // if
        SYMBOL_INT              = 39, // int
        SYMBOL_START            = 40, // Start
        SYMBOL_STRING           = 41, // string
        SYMBOL_SWITCH           = 42, // Switch
        SYMBOL_TRUE             = 43, // true
        SYMBOL_WHILE            = 44, // while
        SYMBOL_ASSIGN           = 45, // <assign>
        SYMBOL_BOOLEAN_LITERAL  = 46, // <boolean_literal>
        SYMBOL_COND             = 47, // <cond>
        SYMBOL_DATA             = 48, // <data>
        SYMBOL_DIGIT2           = 49, // <digit>
        SYMBOL_DO_WHILE         = 50, // <do_while>
        SYMBOL_EXP              = 51, // <exp>
        SYMBOL_EXPR             = 52, // <expr>
        SYMBOL_FACTOR           = 53, // <factor>
        SYMBOL_FOR2             = 54, // <for>
        SYMBOL_ID2              = 55, // <id>
        SYMBOL_IF2              = 56, // <if>
        SYMBOL_INTEGER_LITERAL  = 57, // <integer_literal>
        SYMBOL_LITERAL          = 58, // <literal>
        SYMBOL_OP               = 59, // <op>
        SYMBOL_PROGRAM          = 60, // <program>
        SYMBOL_STATEMENT        = 61, // <statement>
        SYMBOL_STATEMENT_LIST   = 62, // <statement_list>
        SYMBOL_STEP             = 63, // <step>
        SYMBOL_STRING_LITERAL   = 64, // <string_literal>
        SYMBOL_SWITCH2          = 65, // <switch>
        SYMBOL_SWITCH_CASE      = 66, // <switch_case>
        SYMBOL_SWITCH_CASE_LIST = 67, // <switch_case_list>
        SYMBOL_TERM             = 68, // <term>
        SYMBOL_WHILE2           = 69  // <while>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                                        =  0, // <program> ::= Start <statement_list> End
        RULE_STATEMENT_LIST                                                           =  1, // <statement_list> ::= <statement>
        RULE_STATEMENT_LIST2                                                          =  2, // <statement_list> ::= <statement> <statement_list>
        RULE_STATEMENT                                                                =  3, // <statement> ::= <assign>
        RULE_STATEMENT2                                                               =  4, // <statement> ::= <if>
        RULE_STATEMENT3                                                               =  5, // <statement> ::= <for>
        RULE_STATEMENT4                                                               =  6, // <statement> ::= <switch>
        RULE_ASSIGN_EQ_SEMI                                                           =  7, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                                                    =  8, // <id> ::= Id
        RULE_EXPR_PLUS                                                                =  9, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                                               = 10, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                                     = 11, // <expr> ::= <term>
        RULE_TERM_TIMES                                                               = 12, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                                 = 13, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                                             = 14, // <term> ::= <term> '%' <factor>
        RULE_FACTOR_TIMESTIMES                                                        = 15, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                                                   = 16, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                                        = 17, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                                      = 18, // <exp> ::= <id>
        RULE_EXP2                                                                     = 19, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                                              = 20, // <digit> ::= Digit
        RULE_IF_IF_LPAREN_RPAREN_START_END                                            = 21, // <if> ::= if '(' <cond> ')' Start <statement_list> End
        RULE_IF_IF_LPAREN_RPAREN_START_END_ELSE                                       = 22, // <if> ::= if '(' <cond> ')' Start <statement_list> End else <statement_list>
        RULE_COND                                                                     = 23, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                                                    = 24, // <op> ::= '<'
        RULE_OP_GT                                                                    = 25, // <op> ::= '>'
        RULE_OP_EQEQ                                                                  = 26, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                                              = 27, // <op> ::= '!='
        RULE_FOR_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE                            = 28, // <for> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <statement_list> '}'
        RULE_DATA_INT                                                                 = 29, // <data> ::= int
        RULE_DATA_FLOAT                                                               = 30, // <data> ::= float
        RULE_DATA_DOUBLE                                                              = 31, // <data> ::= double
        RULE_DATA_STRING                                                              = 32, // <data> ::= string
        RULE_STEP_MINUSMINUS                                                          = 33, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                                         = 34, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                                            = 35, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                                           = 36, // <step> ::= <id> '++'
        RULE_STEP                                                                     = 37, // <step> ::= <assign>
        RULE_SWITCH_SWITCH_LPAREN_RPAREN_START_END                                    = 38, // <switch> ::= Switch '(' <expr> ')' Start <switch_case_list> End
        RULE_SWITCH_CASE_LIST                                                         = 39, // <switch_case_list> ::= <switch_case> <switch_case_list>
        RULE_SWITCH_CASE_LIST2                                                        = 40, // <switch_case_list> ::= <switch_case>
        RULE_SWITCH_CASE_CASE_COLON                                                   = 41, // <switch_case> ::= Case <literal> ':' <statement_list>
        RULE_SWITCH_CASE_DEFAULT_COLON                                                = 42, // <switch_case> ::= Default ':' <statement_list>
        RULE_LITERAL                                                                  = 43, // <literal> ::= <integer_literal>
        RULE_LITERAL2                                                                 = 44, // <literal> ::= <string_literal>
        RULE_LITERAL3                                                                 = 45, // <literal> ::= <boolean_literal>
        RULE_INTEGER_LITERAL_PLUS                                                     = 46, // <integer_literal> ::= <digit> '+'
        RULE_STRING_LITERAL_QUOTE_LBRACKET_CARET_BACKSLASH_QUOTE_RBRACKET_TIMES_QUOTE = 47, // <string_literal> ::= '"' '[' '^' '\' '"' ']' '*' '"'
        RULE_BOOLEAN_LITERAL_TRUE                                                     = 48, // <boolean_literal> ::= true
        RULE_BOOLEAN_LITERAL_FALSE                                                    = 49, // <boolean_literal> ::= false
        RULE_WHILE_WHILE_LPAREN_RPAREN_START_END                                      = 50, // <while> ::= while '(' <cond> ')' Start <statement_list> End
        RULE_DO_WHILE_DO_START_END_WHILE_LPAREN_RPAREN_SEMI                           = 51  // <do_while> ::= do Start <statement_list> End while '(' <cond> ')' ';'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lstErrors;
        public MyParser(string filename, ListBox lstErrors)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
            this.lstErrors = lstErrors;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUOTE :
                //'"'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BACKSLASH :
                //'\'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CARET :
                //'^'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //Case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //Default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FALSE :
                //false
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //For
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //Switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOLEAN_LITERAL :
                //<boolean_literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO_WHILE :
                //<do_while>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR2 :
                //<for>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF2 :
                //<if>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER_LITERAL :
                //<integer_literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LITERAL :
                //<literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT_LIST :
                //<statement_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING_LITERAL :
                //<string_literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH2 :
                //<switch>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_CASE :
                //<switch_case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_CASE_LIST :
                //<switch_case_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE2 :
                //<while>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <statement_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST :
                //<statement_list> ::= <statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST2 :
                //<statement_list> ::= <statement> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<statement> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<statement> ::= <if>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<statement> ::= <for>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<statement> ::= <switch>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_START_END :
                //<if> ::= if '(' <cond> ')' Start <statement_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_START_END_ELSE :
                //<if> ::= if '(' <cond> ')' Start <statement_list> End else <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for> ::= For '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <statement_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_SWITCH_LPAREN_RPAREN_START_END :
                //<switch> ::= Switch '(' <expr> ')' Start <switch_case_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_LIST :
                //<switch_case_list> ::= <switch_case> <switch_case_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_LIST2 :
                //<switch_case_list> ::= <switch_case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_CASE_COLON :
                //<switch_case> ::= Case <literal> ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_DEFAULT_COLON :
                //<switch_case> ::= Default ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL :
                //<literal> ::= <integer_literal>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL2 :
                //<literal> ::= <string_literal>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL3 :
                //<literal> ::= <boolean_literal>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INTEGER_LITERAL_PLUS :
                //<integer_literal> ::= <digit> '+'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STRING_LITERAL_QUOTE_LBRACKET_CARET_BACKSLASH_QUOTE_RBRACKET_TIMES_QUOTE :
                //<string_literal> ::= '"' '[' '^' '\' '"' ']' '*' '"'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BOOLEAN_LITERAL_TRUE :
                //<boolean_literal> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BOOLEAN_LITERAL_FALSE :
                //<boolean_literal> ::= false
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_WHILE_LPAREN_RPAREN_START_END :
                //<while> ::= while '(' <cond> ')' Start <statement_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DO_WHILE_DO_START_END_WHILE_LPAREN_RPAREN_SEMI :
                //<do_while> ::= do Start <statement_list> End while '(' <cond> ')' ';'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
            string message2 = "Expected token: '" + args.ExpectedTokens.ToString() + "'";
            lstErrors.Items.Add(message2);
        }

    }
}
