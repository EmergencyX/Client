// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: EmergencyExplorerService.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from EmergencyExplorerService.proto</summary>
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
public static partial class EmergencyExplorerServiceReflection
{

	#region Descriptor
	/// <summary>File descriptor for EmergencyExplorerService.proto</summary>
	public static pbr::FileDescriptor Descriptor
	{
		get { return descriptor; }
	}
	private static pbr::FileDescriptor descriptor;

	static EmergencyExplorerServiceReflection()
	{
		byte[] descriptorData = global::System.Convert.FromBase64String(
		    string.Concat(
		      "Ch5FbWVyZ2VuY3lFeHBsb3JlclNlcnZpY2UucHJvdG8iRwoMTG9naW5SZXF1",
		      "ZXN0EhAKCHVzZXJuYW1lGAEgASgJEhAKCHBhc3N3b3JkGAIgASgJEhMKC3Jl",
		      "bWVtYmVyX21lGAMgASgIIjcKFUxvZ2luV2l0aFRva2VuUmVxdWVzdBIPCgd1",
		      "c2VyX2lkGAEgASgNEg0KBXRva2VuGAIgASgJIkAKDUxvZ2luUmVzcG9uc2US",
		      "DwoHc3VjY2VzcxgBIAEoCBIPCgd1c2VyX2lkGAIgASgNEg0KBXRva2VuGAMg",
		      "ASgJMnwKGEVtZXJnZW5jeUV4cGxvcmVyU2VydmljZRImCgVMb2dpbhINLkxv",
		      "Z2luUmVxdWVzdBoOLkxvZ2luUmVzcG9uc2USOAoOTG9naW5XaXRoVG9rZW4S",
		      "Fi5Mb2dpbldpdGhUb2tlblJlcXVlc3QaDi5Mb2dpblJlc3BvbnNlYgZwcm90",
		      "bzM="));
		descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
		    new pbr::FileDescriptor[] { },
		    new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
	  new pbr::GeneratedCodeInfo(typeof(global::LoginRequest), global::LoginRequest.Parser, new[]{ "Username", "Password", "RememberMe" }, null, null, null),
	  new pbr::GeneratedCodeInfo(typeof(global::LoginWithTokenRequest), global::LoginWithTokenRequest.Parser, new[]{ "UserId", "Token" }, null, null, null),
	  new pbr::GeneratedCodeInfo(typeof(global::LoginResponse), global::LoginResponse.Parser, new[]{ "Success", "UserId", "Token" }, null, null, null)
		    }));
	}
	#endregion

}
#region Messages
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
public sealed partial class LoginRequest : pb::IMessage<LoginRequest>
{
	private static readonly pb::MessageParser<LoginRequest> _parser = new pb::MessageParser<LoginRequest>(() => new LoginRequest());
	public static pb::MessageParser<LoginRequest> Parser { get { return _parser; } }

	public static pbr::MessageDescriptor Descriptor
	{
		get { return global::EmergencyExplorerServiceReflection.Descriptor.MessageTypes[0]; }
	}

	pbr::MessageDescriptor pb::IMessage.Descriptor
	{
		get { return Descriptor; }
	}

	public LoginRequest()
	{
		OnConstruction();
	}

	partial void OnConstruction();

	public LoginRequest(LoginRequest other) : this()
	{
		username_ = other.username_;
		password_ = other.password_;
		rememberMe_ = other.rememberMe_;
	}

	public LoginRequest Clone()
	{
		return new LoginRequest(this);
	}

	/// <summary>Field number for the "username" field.</summary>
	public const int UsernameFieldNumber = 1;
	private string username_ = "";
	public string Username
	{
		get { return username_; }
		set
		{
			username_ = pb::Preconditions.CheckNotNull(value, "value");
		}
	}

	/// <summary>Field number for the "password" field.</summary>
	public const int PasswordFieldNumber = 2;
	private string password_ = "";
	public string Password
	{
		get { return password_; }
		set
		{
			password_ = pb::Preconditions.CheckNotNull(value, "value");
		}
	}

	/// <summary>Field number for the "remember_me" field.</summary>
	public const int RememberMeFieldNumber = 3;
	private bool rememberMe_;
	public bool RememberMe
	{
		get { return rememberMe_; }
		set
		{
			rememberMe_ = value;
		}
	}

	public override bool Equals(object other)
	{
		return Equals(other as LoginRequest);
	}

	public bool Equals(LoginRequest other)
	{
		if (ReferenceEquals(other, null))
		{
			return false;
		}
		if (ReferenceEquals(other, this))
		{
			return true;
		}
		if (Username != other.Username) return false;
		if (Password != other.Password) return false;
		if (RememberMe != other.RememberMe) return false;
		return true;
	}

	public override int GetHashCode()
	{
		int hash = 1;
		if (Username.Length != 0) hash ^= Username.GetHashCode();
		if (Password.Length != 0) hash ^= Password.GetHashCode();
		if (RememberMe != false) hash ^= RememberMe.GetHashCode();
		return hash;
	}

	public override string ToString()
	{
		return pb::JsonFormatter.ToDiagnosticString(this);
	}

	public void WriteTo(pb::CodedOutputStream output)
	{
		if (Username.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Username);
		}
		if (Password.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Password);
		}
		if (RememberMe != false)
		{
			output.WriteRawTag(24);
			output.WriteBool(RememberMe);
		}
	}

	public int CalculateSize()
	{
		int size = 0;
		if (Username.Length != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
		}
		if (Password.Length != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
		}
		if (RememberMe != false)
		{
			size += 1 + 1;
		}
		return size;
	}

	public void MergeFrom(LoginRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		if (other.Password.Length != 0)
		{
			Password = other.Password;
		}
		if (other.RememberMe != false)
		{
			RememberMe = other.RememberMe;
		}
	}

	public void MergeFrom(pb::CodedInputStream input)
	{
		uint tag;
		while ((tag = input.ReadTag()) != 0)
		{
			switch (tag)
			{
				default:
					input.SkipLastField();
					break;
				case 10:
					{
						Username = input.ReadString();
						break;
					}
				case 18:
					{
						Password = input.ReadString();
						break;
					}
				case 24:
					{
						RememberMe = input.ReadBool();
						break;
					}
			}
		}
	}

}

[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
public sealed partial class LoginWithTokenRequest : pb::IMessage<LoginWithTokenRequest>
{
	private static readonly pb::MessageParser<LoginWithTokenRequest> _parser = new pb::MessageParser<LoginWithTokenRequest>(() => new LoginWithTokenRequest());
	public static pb::MessageParser<LoginWithTokenRequest> Parser { get { return _parser; } }

	public static pbr::MessageDescriptor Descriptor
	{
		get { return global::EmergencyExplorerServiceReflection.Descriptor.MessageTypes[1]; }
	}

	pbr::MessageDescriptor pb::IMessage.Descriptor
	{
		get { return Descriptor; }
	}

	public LoginWithTokenRequest()
	{
		OnConstruction();
	}

	partial void OnConstruction();

	public LoginWithTokenRequest(LoginWithTokenRequest other) : this()
	{
		userId_ = other.userId_;
		token_ = other.token_;
	}

	public LoginWithTokenRequest Clone()
	{
		return new LoginWithTokenRequest(this);
	}

	/// <summary>Field number for the "user_id" field.</summary>
	public const int UserIdFieldNumber = 1;
	private uint userId_;
	public uint UserId
	{
		get { return userId_; }
		set
		{
			userId_ = value;
		}
	}

	/// <summary>Field number for the "token" field.</summary>
	public const int TokenFieldNumber = 2;
	private string token_ = "";
	public string Token
	{
		get { return token_; }
		set
		{
			token_ = pb::Preconditions.CheckNotNull(value, "value");
		}
	}

	public override bool Equals(object other)
	{
		return Equals(other as LoginWithTokenRequest);
	}

	public bool Equals(LoginWithTokenRequest other)
	{
		if (ReferenceEquals(other, null))
		{
			return false;
		}
		if (ReferenceEquals(other, this))
		{
			return true;
		}
		if (UserId != other.UserId) return false;
		if (Token != other.Token) return false;
		return true;
	}

	public override int GetHashCode()
	{
		int hash = 1;
		if (UserId != 0) hash ^= UserId.GetHashCode();
		if (Token.Length != 0) hash ^= Token.GetHashCode();
		return hash;
	}

	public override string ToString()
	{
		return pb::JsonFormatter.ToDiagnosticString(this);
	}

	public void WriteTo(pb::CodedOutputStream output)
	{
		if (UserId != 0)
		{
			output.WriteRawTag(8);
			output.WriteUInt32(UserId);
		}
		if (Token.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Token);
		}
	}

	public int CalculateSize()
	{
		int size = 0;
		if (UserId != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeUInt32Size(UserId);
		}
		if (Token.Length != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeStringSize(Token);
		}
		return size;
	}

	public void MergeFrom(LoginWithTokenRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.UserId != 0)
		{
			UserId = other.UserId;
		}
		if (other.Token.Length != 0)
		{
			Token = other.Token;
		}
	}

	public void MergeFrom(pb::CodedInputStream input)
	{
		uint tag;
		while ((tag = input.ReadTag()) != 0)
		{
			switch (tag)
			{
				default:
					input.SkipLastField();
					break;
				case 8:
					{
						UserId = input.ReadUInt32();
						break;
					}
				case 18:
					{
						Token = input.ReadString();
						break;
					}
			}
		}
	}

}

[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
public sealed partial class LoginResponse : pb::IMessage<LoginResponse>
{
	private static readonly pb::MessageParser<LoginResponse> _parser = new pb::MessageParser<LoginResponse>(() => new LoginResponse());
	public static pb::MessageParser<LoginResponse> Parser { get { return _parser; } }

	public static pbr::MessageDescriptor Descriptor
	{
		get { return global::EmergencyExplorerServiceReflection.Descriptor.MessageTypes[2]; }
	}

	pbr::MessageDescriptor pb::IMessage.Descriptor
	{
		get { return Descriptor; }
	}

	public LoginResponse()
	{
		OnConstruction();
	}

	partial void OnConstruction();

	public LoginResponse(LoginResponse other) : this()
	{
		success_ = other.success_;
		userId_ = other.userId_;
		token_ = other.token_;
	}

	public LoginResponse Clone()
	{
		return new LoginResponse(this);
	}

	/// <summary>Field number for the "success" field.</summary>
	public const int SuccessFieldNumber = 1;
	private bool success_;
	public bool Success
	{
		get { return success_; }
		set
		{
			success_ = value;
		}
	}

	/// <summary>Field number for the "user_id" field.</summary>
	public const int UserIdFieldNumber = 2;
	private uint userId_;
	public uint UserId
	{
		get { return userId_; }
		set
		{
			userId_ = value;
		}
	}

	/// <summary>Field number for the "token" field.</summary>
	public const int TokenFieldNumber = 3;
	private string token_ = "";
	public string Token
	{
		get { return token_; }
		set
		{
			token_ = pb::Preconditions.CheckNotNull(value, "value");
		}
	}

	public override bool Equals(object other)
	{
		return Equals(other as LoginResponse);
	}

	public bool Equals(LoginResponse other)
	{
		if (ReferenceEquals(other, null))
		{
			return false;
		}
		if (ReferenceEquals(other, this))
		{
			return true;
		}
		if (Success != other.Success) return false;
		if (UserId != other.UserId) return false;
		if (Token != other.Token) return false;
		return true;
	}

	public override int GetHashCode()
	{
		int hash = 1;
		if (Success != false) hash ^= Success.GetHashCode();
		if (UserId != 0) hash ^= UserId.GetHashCode();
		if (Token.Length != 0) hash ^= Token.GetHashCode();
		return hash;
	}

	public override string ToString()
	{
		return pb::JsonFormatter.ToDiagnosticString(this);
	}

	public void WriteTo(pb::CodedOutputStream output)
	{
		if (Success != false)
		{
			output.WriteRawTag(8);
			output.WriteBool(Success);
		}
		if (UserId != 0)
		{
			output.WriteRawTag(16);
			output.WriteUInt32(UserId);
		}
		if (Token.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Token);
		}
	}

	public int CalculateSize()
	{
		int size = 0;
		if (Success != false)
		{
			size += 1 + 1;
		}
		if (UserId != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeUInt32Size(UserId);
		}
		if (Token.Length != 0)
		{
			size += 1 + pb::CodedOutputStream.ComputeStringSize(Token);
		}
		return size;
	}

	public void MergeFrom(LoginResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Success != false)
		{
			Success = other.Success;
		}
		if (other.UserId != 0)
		{
			UserId = other.UserId;
		}
		if (other.Token.Length != 0)
		{
			Token = other.Token;
		}
	}

	public void MergeFrom(pb::CodedInputStream input)
	{
		uint tag;
		while ((tag = input.ReadTag()) != 0)
		{
			switch (tag)
			{
				default:
					input.SkipLastField();
					break;
				case 8:
					{
						Success = input.ReadBool();
						break;
					}
				case 16:
					{
						UserId = input.ReadUInt32();
						break;
					}
				case 26:
					{
						Token = input.ReadString();
						break;
					}
			}
		}
	}

}

#endregion


#endregion Designer generated code