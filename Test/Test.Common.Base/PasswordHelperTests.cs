//-----------------------------------------------------------------------------
// <copyright file="PasswordHelperTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Common.Base
{
    using Codev.Core.Base;
    using Microsoft.Extensions.DependencyModel;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the Common Base PasswordHelper class.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PasswordHelperTests
    {
        #region Setup
        ///---------------------------------------------------------------
        /// <summary>
        /// Setup the class.
        /// </summary>
        ///---------------------------------------------------------------
        [SetUp]
        public void Setup()
        {
        }
        #endregion

        #region Tests (String Related)
        ///---------------------------------------------------------------
        /// <summary>
        /// Test that the salt generation of zero throws exception.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void SaltGenerationIterationsZero()
        {
            try
            {
                PasswordHelper.GenerateSalt(0);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that the salt generation is always 48 characters long.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void SaltGenerationSize()
        {
            for (Int32 idx = 1; idx < 50; idx++)
            {
                String salt = PasswordHelper.GenerateSalt(idx);

                Assert.AreEqual(48, salt.Length);
            }

            Assert.Pass();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that the salt generation is always unique.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void SaltGenerationUniqueness()
        {
            HashSet<String> salts = new HashSet<String>();

            for (Int32 idx = 1; idx < 50; idx++)
            {
                String salt = PasswordHelper.GenerateSalt(idx);

                Boolean added = salts.Add(salt);

                Assert.IsTrue(added);
            }

            Assert.Pass();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that the salt generation is unique even with same
        /// iteration count.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void SaltGenerationUniquenessSameInteration()
        {
            String salt0 = PasswordHelper.GenerateSalt(10);
            String salt1 = PasswordHelper.GenerateSalt(10);

            Assert.AreNotEqual(salt0, salt1);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test a basic password hash and salt generation.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationSize()
        {
            for (Int32 idx = 1; idx < 50; idx++)
            {
                String salt = PasswordHelper.GenerateSalt(idx);

                String hash = PasswordHelper.GenerateHash(String.Format("password{0}", idx), salt);

                Assert.AreEqual(88, hash.Length);
            }

            Assert.Pass();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that hash on an empty password.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationPasswordEmpty()
        {
            String salt = PasswordHelper.GenerateSalt(10);

            try
            {
                PasswordHelper.GenerateHash(String.Empty, salt);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that hash on an empty salt.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationSaltEmpty()
        {
            try
            {
                PasswordHelper.GenerateHash("SomePassword", String.Empty);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test that hash generates same with same salt.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationConsistency()
        {
            String salt = PasswordHelper.GenerateSalt(10);

            String hash = PasswordHelper.GenerateHash("SamePassword", salt);

            for (Int32 idx = 0; idx < 4; idx++)
            {
                String hashSame = PasswordHelper.GenerateHash("SamePassword", salt);

                Int32 compare = String.Compare(hash, hashSame);

                Assert.AreEqual(0, compare);
            }

            Assert.Pass();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test hash uniqueness.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationUniqueness()
        {
            HashSet<String> hashes = new HashSet<String>();

            for (Int32 idx = 1; idx < 50; idx++)
            {
                String salt = PasswordHelper.GenerateSalt(idx);

                String hash = PasswordHelper.GenerateHash(String.Format("password{0}", idx), salt);

                Boolean added = hashes.Add(hash);

                Assert.IsTrue(added);
            }

            Assert.Pass();
        }
        #endregion

        ///---------------------------------------------------------------
        /// <summary>
        /// Test hash uniqueness.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationByteSaltLength()
        {
            Byte[] salt = PasswordHelper.GenerateRandomSalt(10);

            Assert.AreEqual(36, salt.Length);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Test the Salt generation with a byte array and verify the 
        /// iteration count is correct.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void HashGenerationByteSaltIteration()
        {
            Byte[] salt = PasswordHelper.GenerateRandomSalt(10);

            Byte[] interationArray = [0, 0, 0, 0];

            Buffer.BlockCopy(salt, 0, interationArray, 0, 4);

            Int32 iterations = BitConverter.ToInt32(interationArray);

            Assert.AreEqual(36, salt.Length);
            Assert.AreEqual(10, iterations);
        }
    }
}